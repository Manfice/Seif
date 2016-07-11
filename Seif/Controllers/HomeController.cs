using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seif.Repos;

namespace Seif.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IProducts _products;

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

        public ActionResult GunCase()
        {
            return View(_products.GetGunsCase());
        }
    }
}