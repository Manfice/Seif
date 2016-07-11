using System.Data.Entity;
using Seif.Models.SeifData;

namespace Seif.Models
{
    public class DataContext:DbContext
    {
        public DataContext():base("ferocon_db")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}