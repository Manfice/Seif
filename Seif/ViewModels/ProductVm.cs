using System.Collections.Generic;
using Seif.Models.SeifData;

namespace Seif.ViewModels
{
    public class ProductVm
    {
        public Product Product { get; set; }
        public ProductImage Avatar { get; set; }
        public IEnumerable<ProductImage> Photos { get; set; } 
    }
}