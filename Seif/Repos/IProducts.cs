using System.Collections.Generic;
using Seif.Models.SeifData;

namespace Seif.Repos
{
    public interface IProducts
    {
        IEnumerable<Catalog> Catalogs { get; }
        Catalog CatalogItem(int id);
        IEnumerable<CatalogItem> CatalogItems(int id);
        IEnumerable<Product> GetAllProductsInGroupe(int gId);
        IEnumerable<Product> GetProductsInCategory(int cId);
        IEnumerable<Product> GetGunsCase();
        Product GetProduct(int id);
    }
}