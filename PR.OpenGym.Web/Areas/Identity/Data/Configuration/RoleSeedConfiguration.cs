using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.OpenGym.Data.Literals;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "28fc5ae8-c637-4bfd-8aa4-23c785a28bfb",
                    Name = Constants.ADMIN,
                    NormalizedName = Constants.ADMIN.ToUpper(),
                },
                new IdentityRole
                {
                    Id = "257020d9-bc80-4821-a6ca-ffef7c787dfa",
                    Name = Constants.USER,
                    NormalizedName = Constants.USER.ToUpper(),
                });
        }
    }
}
