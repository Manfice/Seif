using System;
using System.Collections.Generic;
using System.Linq;
using Seif.Models;
using Seif.Models.SeifData;
using Seif.Repos;
using Seif.ViewModels;

namespace Seif.Realize
{
    public class DbProducts : IProducts
    {
        private readonly DataContext _context = new DataContext();

        public IEnumerable<Catalog> Catalogs => _context.Catalogs.ToList();

        public Catalog CatalogItem(int id)
        {
            return _context.Catalogs.Find(id);
        }

        public IEnumerable<CatalogItem> CatalogItems(int id)
        {
            return _context.CatalogItems.Where(item => item.Catalog.Id == id).ToList();
        }

        public IEnumerable<Product> GetAllProductsInGroupe(int gId)
        {
            return _context.Products.Where(product => product.CatalogItem.Catalog.Id == gId).ToList();
        }

        public IEnumerable<Product> GetGunsCase()
        {
            return _context.Products.Where(product => product.CatalogItem.Catalog.Id == 1).ToList();
        }

        public Order GetOrder(Guid id)
        {
            return _context.Orders.FirstOrDefault(order => order.CheckOrderTocken==id);
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetProductsInCategory(int cId)
        {
            return _context.Products.Where(product => product.CatalogItem.Id==cId).ToList();
        }

        public Guid MakeOrder(CartViewModel cart)
        {
            var ord = new Order
            {
                Email = cart.EMail,
                Name = cart.Name,
                OrderDAte = DateTime.Now,
                Paid = false,
                Phone = cart.Phone,
                CheckOrderTocken = Guid.NewGuid()
            };
            _context.Orders.Add(ord);
            foreach (var item in cart.Cart.CartItems)
            {
                item.Order = ord;
                _context.CartItems.Add(item);
            }
            _context.SaveChanges();
            return ord.CheckOrderTocken;
        }
    }
}