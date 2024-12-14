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
    }
}
