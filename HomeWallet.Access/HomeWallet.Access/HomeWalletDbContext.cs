using HomeWallet.Domain.Enteties;
using Microsoft.EntityFrameworkCore;

namespace HomeWallet.Access
{
    public class HomeWalletDbContext : DbContext
    {
        public HomeWalletDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Operations);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Operations)
                .WithOne(x => x.Category).OnDelete(DeleteBehavior.NoAction);
        }
    }
}