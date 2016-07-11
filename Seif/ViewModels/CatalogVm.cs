using System.Collections.Generic;
using System.Web;
using Seif.Models.SeifData;

namespace Seif.ViewModels
{
    public class CatalogVm
    {
        public Catalog Catalog { get; set; }
        public HttpPostedFileBase CatImage { get; set; }
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
    }
}