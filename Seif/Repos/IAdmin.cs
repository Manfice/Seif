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

    }
}