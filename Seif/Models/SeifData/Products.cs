using System.Collections.Generic;
using System.Web.Mvc;

namespace Seif.Models.SeifData
{
    public class Products
    {
        public Catalog Catalog { get; set; }
        public virtual IEnumerable<CatalogItem> CatalogItems { get; set; }
        public virtual IEnumerable<Product> ProductsList { get; set; }
    }

    public class Catalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        [AllowHtml]
        public string HtmlDescr { get; set; }
        public string Image { get; set; }
        public string LinkTo { get; set; }
        public virtual ICollection<CatalogItem> CatalogItems { get; set; } 
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
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Articul { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        public decimal Price { get; set; }
        [AllowHtml]
        public string HtmlDescr { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depht { get; set; }
        public decimal Weight { get; set; }
        public bool Hit { get; set; }
        public decimal Discount { get; set; }
        public virtual CatalogItem CatalogItem { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }

    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public PhotoType PhotoType { get; set; }
        public virtual Product Product { get; set; }
    }

    public enum PhotoType
    {
        Avatar, Photo
    }
}