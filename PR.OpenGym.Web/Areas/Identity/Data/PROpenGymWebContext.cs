using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PR.OpenGym.Data;
using PR.OpenGym.Web.Areas.Identity.Data;
using PR.OpenGym.Web.Areas.Identity.Data.Configuration;

namespace PR.OpenGym.Web.Data;

public class PROpenGymWebContext : IdentityDbContext<PROpenGymWebUser>
{
    public DbSet<Associate> Associates { get; set; }
    public DbSet<AssociateDetails> AssociateDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<AssociateMembership> AssociateMemberships { get; set; }
    public PROpenGymWebContext(DbContextOptions<PROpenGymWebContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Seed
        builder.ApplyConfiguration(new RoleSeedConfiguration());
        builder.ApplyConfiguration(new UserSeedConfiguration());
        builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        builder.ApplyConfiguration(new MembershipSeedConfiguration());
        builder.ApplyConfiguration(new ProductsSeedConfiguration());

        builder.ApplyConfiguration(new BranchSeedConfiguration());

        builder.ApplyConfiguration(new AssociateSeedConfiguration());
        builder.ApplyConfiguration(new AssociateMembershipSeedConfiguration());

    }

}
