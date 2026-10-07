using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "57c7963f-c1fb-48b9-847a-38deadd11453",
                    RoleId = "28fc5ae8-c637-4bfd-8aa4-23c785a28bfb"
                },
                new IdentityUserRole<string>
                {
                    UserId = "20e6c427-d499-4fb3-b53b-0e44d99767b7",
                    RoleId = "257020d9-bc80-4821-a6ca-ffef7c787dfa"
                });
        }
    }
}