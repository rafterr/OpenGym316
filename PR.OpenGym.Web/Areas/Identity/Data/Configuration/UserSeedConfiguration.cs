using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PR.OpenGym.Web.Areas.Identity.Data.Configuration
{
    internal class UserSeedConfiguration : IEntityTypeConfiguration<PROpenGymWebUser>
    {
        public void Configure(EntityTypeBuilder<PROpenGymWebUser> builder)
        {
            var password = new PasswordHasher<IdentityUser>();
            var hashPassword = password.HashPassword(null, "123");
            builder.HasData(
                new PROpenGymWebUser
                {
                    Id = "57c7963f-c1fb-48b9-847a-38deadd11453",
                    UserName = "admin@316fitness.com",
                    NormalizedUserName  = "ADMIN@316FITNESS.COM",
                    PasswordHash = hashPassword,
                    EmailConfirmed = true,
                },
                 new PROpenGymWebUser
                 {
                     Id = "20e6c427-d499-4fb3-b53b-0e44d99767b7",
                     UserName = "user@316fitness.com",
                     NormalizedUserName = "USER@316FITNESS.COM",
                     PasswordHash = hashPassword,
                     EmailConfirmed = true,
                 }
                );
        }
    }
}