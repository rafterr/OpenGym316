using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    public class BranchSeedConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(
                new Branch()
                {   
                    Id=1,
                    Address = "Blvd. la Luz, Las Cruces, 37290 León, Gto.",
                    Name = "3:16 Fitness - Río Mayo Delta"
                });
        }
    }
}