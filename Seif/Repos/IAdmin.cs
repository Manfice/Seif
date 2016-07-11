using System.Collections.Generic;
using Seif.Models.SeifData;
using Seif.ViewModels;

namespace Seif.Repos
{
    public interface IAdmin
    {
        int Login(LoginViewModel model);
        User GetUser(int id);
        IEnumerable<Catalog> GetCatalog { get; }
        void GreateCatalog(Catalog model);
        Catalog CatDetails(int id);
        void SaveCatalogData(Catalog model);
        IEnumerable<CatalogItem> GetCatalogItems(int id);
        CatalogItem GetCatalogItem(int id);
        void UpdateCatItem(CatalogItem model);
        Product GetProduct(int id);
        void EditProductHead(Product model, ProductImage img);
        int CreateProduct(Product model, ProductImage ava);

    }
}