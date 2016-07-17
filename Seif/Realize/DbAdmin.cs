using System;
using System.Collections.Generic;
using System.Linq;
using Seif.Models;
using Seif.Models.SeifData;
using Seif.Repos;
using Seif.ViewModels;

namespace Seif.Realize
{
    public class DbAdmin:IAdmin
    {
        private readonly DataContext _context = new DataContext();

        public IEnumerable<Catalog> GetCatalog => _context.Catalogs.ToList();

        public int Login(ViewModels.LoginViewModel model)
        {
            var usr = _context.Users.FirstOrDefault(user => user.UserName == model.UserName && user.Password == model.Password);
            return usr?.Id ?? -1;

        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void GreateCatalog(Catalog model)
        {
            var item = new Catalog
            {
                Title = model.Title,
                Descroption = model.Descroption,
                HtmlDescr = model.HtmlDescr
            };
            _context.Catalogs.Add(item);
            _context.SaveChanges();
        }

        public Catalog CatDetails(int id)
        {
            return _context.Catalogs.Find(id);
        }

        public void SaveCatalogData(Catalog model)
        {
            var dbCatalog = _context.Catalogs.Find(model.Id);
            dbCatalog.Title = model.Title;
            dbCatalog.Descroption = model.Descroption;
            dbCatalog.HtmlDescr = model.HtmlDescr;
            dbCatalog.Image = model.Image;
            _context.SaveChanges();
        }

        public IEnumerable<CatalogItem> GetCatalogItems(int id)
        {
            return _context.CatalogItems.Where(item => item.Catalog.Id == id).ToList();
        }

        public CatalogItem GetCatalogItem(int id)
        {
            return _context.CatalogItems.Find(id);
        }

        public void UpdateCatItem(CatalogItem model)
        {
            var dbCatalogItem = _context.CatalogItems.Find(model.Id);
            dbCatalogItem.Title = model.Title;
            dbCatalogItem.Descroption = model.Descroption;
            dbCatalogItem.HtmlDescr = model.HtmlDescr;
            dbCatalogItem.Image = model.Image;
            _context.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public void EditProductHead(Product model, ProductImage img)
        {
            var p = _context.Products.Find(model.Id);
            
            if (img!=null)
            {
                var aaa =
                    _context.ProductImages.Where(image => image.Product.Id == p.Id && image.PhotoType == PhotoType.Avatar)
                        .ToList();
                _context.ProductImages.RemoveRange(aaa);
                img.Product = p;
                _context.ProductImages.Add(img);
            }
            p.Title = model.Title;
            p.Descroption = model.Descroption;
            p.HtmlDescr = model.HtmlDescr;
            p.Articul = model.Articul;
            p.Height = model.Height;
            p.Width = model.Width;
            p.Depht = model.Depht;
            p.Weight = model.Weight;
            p.Price = model.Price;
            _context.SaveChanges();
        }

        public int CreateProduct(Product model, ProductImage ava)
        {
            var p = model;
            var cat = _context.CatalogItems.Find(model.CatalogItem.Id);
            p.CatalogItem = cat;
            _context.Products.Add(p);
            ava.Product = p;
            _context.ProductImages.Add(ava);
            _context.SaveChanges();
            return p.CatalogItem.Id;
        }

        public void AddPhoto(ProductImage photo)
        {
            _context.ProductImages.Add(photo);
            _context.SaveChanges();
        }

        public void RemoveImage(int id)
        {
            var i = _context.ProductImages.Find(id);
            _context.ProductImages.Remove(i);
            _context.SaveChanges();
        }

        public ProductImage GetImage(int id)
        {
            return _context.ProductImages.Find(id);
        }
    }
}