using DevStore.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(160).IsRequired();
            Property(x => x.Price).IsRequired();

            HasRequired(x => x.Category);
        }
    }
}
