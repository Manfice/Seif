﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Seif.Models.SeifData;
using Seif.Repos;
using Seif.ViewModels;

namespace Seif.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var model = _admin.GetUser(int.Parse(User.Identity.Name));
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginMe(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login",model);
            }
            var user = _admin.Login(model);
            if (user > 0)
            {
                FormsAuthentication.SetAuthCookie(user.ToString(), true);
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                ViewBag.msg = "Login Error: " +model.Password;
                return View("Login",model);

            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GunCase", "Home");
        }

        public ActionResult CategorysList()
        {
            return View(_admin.GetCatalog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category(Catalog model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Error";
                return View("CategorysList");
            }
            _admin.GreateCatalog(model);
            return RedirectToAction("CategorysList");
        }

        public ActionResult CatData(int id)
        {
            var item = _admin.CatDetails(id);
            var model = new CatalogVm
            {
                Catalog = item,
                CatalogItems = _admin.GetCatalogItems(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCatalog(CatalogVm model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CatData", model);
            }
            if (model.CatImage!=null)
            {
                var fileName = model.CatImage.FileName;
                var filePath = Server.MapPath("/Content/img/goods/UploadsImages");
                var fullPath = Path.Combine(filePath, fileName);
                model.CatImage.SaveAs(fullPath);
                model.Catalog.Image = "/Content/img/goods/UploadImages/" + fileName;
            }
            _admin.SaveCatalogData(model.Catalog);
            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubCat(CatalogItem model,HttpPostedFileBase avatar, string returnUrl)
        {
            return Redirect(returnUrl);
        }

        public ActionResult CatItemDetails(int id)
        {
            var model = _admin.GetCatalogItem(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCatItem(CatalogItem model, HttpPostedFileBase avatar)
        {
            if (avatar!=null)
            {
                var fileName = Guid.NewGuid()+"_"+avatar.FileName;
                var filePath = Server.MapPath("/Content/img/goods/UploadsImages");
                var fullPath = Path.Combine(filePath, fileName);
                avatar.SaveAs(fullPath);
                model.Image = "/Content/img/goods/UploadsImages/" + fileName;
            }
            _admin.UpdateCatItem(model);
            return RedirectToAction("CatItemDetails",model);
        }
        public ActionResult ProductDetails(int id)
        {
            var p = _admin.GetProduct(id);
            var pvm = new ProductVm
            {
                Product = p,
                Avatar = p.Images.FirstOrDefault(image => image.PhotoType==PhotoType.Avatar),
                Photos = p.Images.Where(image => image.PhotoType==PhotoType.Photo).ToList()
            };
            return View(pvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(ProductVm model, HttpPostedFileBase avatar, string returnUrl)
        {
            ProductImage avatarka = null;
            if (avatar != null)
            {
                var fileName = Guid.NewGuid()+"_"+avatar.FileName;
                var filePath = Server.MapPath("/Content/img/goods/UploadsImages");
                var fullPath = Path.Combine(filePath, fileName);
                avatar.SaveAs(fullPath);
                avatarka = new ProductImage
                {
                    Path = "/Content/img/goods/UploadsImages/" + fileName,
                    PhotoType = PhotoType.Avatar
                };
            }
            if (model.Product.Id > 0)
            {
                _admin.EditProductHead(model.Product, avatarka);
            }
            else
            {
                var resalt = _admin.CreateProduct(model.Product, avatarka);
                return RedirectToAction("CatItemDetails", new {id = resalt});
            }
            return Redirect(returnUrl);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoto(int pId, List<HttpPostedFileBase> photo)
        {
            if (photo.Count <= 0) return RedirectToAction("ProductDetails", "Admin", new {id = pId});
            foreach (var item in photo)
            {
                var fileName = Guid.NewGuid()+"_"+item.FileName;
                var filePath = Server.MapPath("/Content/img/goods/UploadsImages");
                var fullPath = Path.Combine(filePath, fileName);
                item.SaveAs(fullPath);
                var phot = new ProductImage
                {
                    Product = _admin.GetProduct(pId),
                    Path = "/Content/img/goods/UploadsImages/" + fileName,
                    PhotoType = PhotoType.Photo
                };
                _admin.AddPhoto(phot);
            }
            return RedirectToAction("ProductDetails","Admin",new {id=pId});
        }

        public ActionResult RemoveImage(int id)
        {
            var i = _admin.GetImage(id);
            if (!System.IO.File.Exists(Server.MapPath(i.Path)))
                return RedirectToAction("ProductDetails", "Admin", new {id = i.Product.Id});
            //var file = System.IO.File.OpenWrite(Server.MapPath(i.Path));
            System.IO.File.Delete(Server.MapPath(i.Path));
            _admin.RemoveImage(i.Id);
            return RedirectToAction("ProductDetails", "Admin", new {id = i.Product.Id});
        }
        public ActionResult AddProduct(int wow)
        {
            var ci = _admin.GetCatalogItem(wow);
            var model = new ProductVm
            {
                Product = new Product
                {
                    CatalogItem = ci
                }
            };
            return View(model);
        }
    }
}