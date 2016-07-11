using System;
using System.Collections.Generic;
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
            return View(_admin.CatDetails(id));
        }
    }
}