using CryptoWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Infra.Context
{
    public class CryptoWatcherContext : DbContext
    {
        public CryptoWatcherContext(DbContextOptions<CryptoWatcherContext> options)
            : base(options)
        {

        }
        public DbSet<CryptoInfo> cryptosInfo { get; set; }
        public DbSet<Profile> profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Cryptos)
                .WithMany(c => c.Profiles)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfileHasCrypto",
                    j => j.HasOne<CryptoInfo>().WithMany().HasForeignKey("CryptoId"),
                    j => j.HasOne<Profile>().WithMany().HasForeignKey("ProfileId")
                    );

            base.OnModelCreating(modelBuilder);
        }
    }
}
