using System.Collections.Generic;
using System.Web.Mvc;

namespace Seif.Models.SeifData
{
    public class Products
    {
        public Catalog Catalog { get; set; }
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
        public IEnumerable<Product> ProductsList { get; set; }
    }

    public class Catalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        [AllowHtml]
        public string HtmlDescr { get; set; }
        public string Image { get; set; }
        public virtual IEnumerable<CatalogItem> CatalogItems { get; set; } 
    }

    public class CatalogItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        [AllowHtml]
        public string HtmlDescr { get; set; }
        public string Image { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        public decimal Price { get; set; }
        [AllowHtml]
        public string HtmlDescr { get; set; }
        public virtual CatalogItem CatalogItem { get; set; }
        public virtual IEnumerable<ProductImage> Images { get; set; }

    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual Product Product { get; set; }
    }
}