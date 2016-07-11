using System.Collections.Generic;
using System.Linq;

namespace Seif.Models.SeifData
{
    public class Cart
    {
        private readonly List<CartItem> _lines = new List<CartItem>();
        public IEnumerable<CartItem> CartItems => _lines.ToList();

        public void ClearCart()
        {
            _lines.Clear();
        }

        public decimal CalcTotal()
        {
            return decimal.Round(_lines.Sum(item => item.Price), 2);
        }

        public void RemoveLine(Product product)
        {
            _lines.RemoveAll(item => item.Product.Id == product.Id);
        }

        public void AddToCart(Product product, int q)
        {
            var line = _lines.FirstOrDefault(item => item.Product.Id == product.Id);
            if (line == null)
            {
                _lines.Add(new CartItem
                {
                    Product = product,
                    Price = product.Price,
                    Quantity = q
                });
            }
            else
            {
                line.Quantity += q;
            }
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal => decimal.Round(Quantity*Price, 2);
    }
}