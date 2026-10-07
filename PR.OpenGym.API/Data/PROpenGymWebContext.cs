using Microsoft.EntityFrameworkCore;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Data
{
    public class PROpenGymWebContext : DbContext
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
    }

}
