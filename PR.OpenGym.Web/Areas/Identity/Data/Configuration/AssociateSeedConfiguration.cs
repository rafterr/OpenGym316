using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    public class AssociateSeedConfiguration : IEntityTypeConfiguration<Associate>
    {
        public void Configure(EntityTypeBuilder<Associate> builder)
        {
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();

            builder.HasData(new Associate()
            {
                Id = 1,
                FirstName = "Admin",
                CreatedOn = now,
                ModifiedOn = now,
                Email = "admin@316fitness.com",
                AssociateMembershipId = 1,
                Status = Status.Active,
            },
            new Associate
            {
                Id = 2,
                FirstName = "Visita",
                CreatedOn = now,
                ModifiedOn = now,
                Email = "no-mail@316fitness.com",
                AssociateMembershipId = 2,
                Status = Status.Active,
            }

            );
        }
    }
}
