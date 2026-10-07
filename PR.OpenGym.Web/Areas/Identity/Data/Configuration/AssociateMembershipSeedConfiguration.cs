using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    public class AssociateMembershipSeedConfiguration : IEntityTypeConfiguration<AssociateMembership>
    {
        public void Configure(EntityTypeBuilder<AssociateMembership> builder)
        {
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            builder.HasData(
                 new AssociateMembership()
                 {
                     Id = 1,
                     CreatedOn = now,
                     From = now,
                     To = now.AddYears(99),
                     MembershipId = 1,
                     ModifiedOn = now,
                 },

                 new AssociateMembership()
                 {
                     Id = 2,
                     CreatedOn = now,
                     From = now,
                     To = now.AddYears(99),
                     MembershipId = 1,
                     ModifiedOn = now,
                 });
        }
    }
}
