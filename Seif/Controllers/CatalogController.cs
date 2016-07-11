using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seif.Models.SeifData;
using Seif.Repos;

namespace Seif.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProducts _products;

        public CatalogController(IProducts products)
        {
            _products = products;
        }
        // GET: Catalog
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = _products.Catalogs;
            return View(model);
        }

        public ActionResult CatDetails(int groupe, int sCat=0)
        {
            var model = new Products
            {
                Catalog = _products.CatalogItem(groupe),
                CatalogItems = _products.CatalogItems(groupe),
                ProductsList =
                    sCat == 0 ? _products.GetAllProductsInGroupe(groupe) : _products.GetProductsInCategory(sCat)
            };

            ViewBag.sCat = sCat;
            return View(model);
        }

        public ActionResult Navigation(int id, int subCat = 0)
        {
            var model = _products.Catalogs;
            ViewBag.Selected = id;
            ViewBag.sCat = subCat;
            return PartialView(model);
        }

        public ActionResult SubNav(int groupe, int sCat=0)
        {
            var model = _products.CatalogItems(groupe);
            ViewBag.CatItem = sCat;
            return PartialView(model);
        }
    }
}