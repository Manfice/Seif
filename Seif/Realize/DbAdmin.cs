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
    }
}