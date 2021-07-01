using DevStore.Domain.Models;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.Infra.DataContext
{
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext() : base("DevStoreConnection")
        {
            Database.SetInitializer(new DevStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext>
    {
        protected override void Seed(DevStoreDataContext context)
        {
            var categoryInformatica = new Category { Title = "Informatica" };
            var categoryGames = new Category { Title = "Games" };
            var categoryPapelaria = new Category { Title = "Papelaria" };

            context.Categories.Add(categoryInformatica);
            context.Categories.Add(categoryGames);
            context.Categories.Add(categoryPapelaria);

            context.SaveChanges();

            context.Products.Add(new Product { CategoryId = 1, IsActive = true, Price = 1000, Title = "Computador de Mesa" });
            context.Products.Add(new Product { CategoryId = 1, IsActive = true, Price = 2000, Title = "NootBook" });
            context.Products.Add(new Product { CategoryId = 1, IsActive = true, Price = 5000, Title = "Monitor LCD" });

            context.Products.Add(new Product { CategoryId = 2, IsActive = true, Price = 1200, Title = "God of War 4" });
            context.Products.Add(new Product { CategoryId = 2, IsActive = true, Price = 100, Title = "Resident Evil 3 Nemesis" });

            context.Products.Add(new Product { CategoryId = 3, IsActive = true, Price = 12, Title = "Caderno 10 Matérias" });
            context.Products.Add(new Product { CategoryId = 3, IsActive = true, Price = 0, Title = "Lapiseira Faber Castel" });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
