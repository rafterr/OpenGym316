using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    internal class ProductsSeedConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.HasData(
            //    new Product()
            //    {
            //        Id = 1,
            //        CreatedOn = DateTime.UtcNow,
            //        ModifiedOn = DateTime.UtcNow,
            //        Name = "Proteina",
            //        Price = 199.99m
            //    },
            //    new Product()
            //    {
            //        Id = 2,
            //        CreatedOn = DateTime.UtcNow,
            //        ModifiedOn = DateTime.UtcNow,
            //        Name = "Inscripcion",
            //        Price = 99.99m
            //    },
            //    new Product()
            //    {
            //        Id = 3,
            //        CreatedOn = DateTime.UtcNow,
            //        ModifiedOn = DateTime.UtcNow,
            //        Name = "Clases",
            //        Price = 99.99m
            //    });
        }
    }
}