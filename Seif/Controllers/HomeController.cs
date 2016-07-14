using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seif.Repos;
using Seif.Models.SeifData;
using Seif.ViewModels;

namespace Seif.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProducts _products;

        public HomeController(IProducts products)
        {
            _products = products;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult GunCase(Cart cart)
        {
            var model = new ProductView
            {
                Products = _products.GetGunsCase(),
                Cart = cart
            };
            return View(model);
        }

        public ActionResult AddToCart(Cart cart, int prodId, int q)
        {
            var p = _products.GetProduct(prodId);
            cart.AddToCart(p,q);
            return RedirectToAction("GunCase");
        }

        public ActionResult CartBlock(Cart cart)
        {
            return PartialView(cart);
        }
    }
}