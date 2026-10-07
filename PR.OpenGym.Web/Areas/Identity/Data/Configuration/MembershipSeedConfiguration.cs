using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    internal class MembershipSeedConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasData(
                new Membership()
                {
                    Id = 1,
                    CreatedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone(),
                    ModifiedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone(),
                    Name = "Mensual",
                    Period = 30,
                    Price = 499.99m,
                    Description = "30 días"
                }, new Membership()
                {
                    Id = 2,
                    CreatedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone(),
                    ModifiedOn = DateTime.Now.ConvertDateToMexicoCentralLocalZone(),
                    Name = "Quincenal",
                    Period = 15,
                    Price = 299.99m,
                    Description = "15 días"
                }
                );
        }
    }
}