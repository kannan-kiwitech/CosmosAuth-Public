using Microsoft.EntityFrameworkCore;

namespace CosmosAuth.Models
{
    public class IdentityContext : DbContext
    {
        public DbSet<Identity> Identities { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options)
      : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Identity>()
                .ToContainer("Identities");
        }
    }
}