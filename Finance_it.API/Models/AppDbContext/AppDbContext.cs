using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Models.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseLazyLoadingProxies();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<FinancialEntry> FinancialEntries { get; set; }
        public DbSet<FinancialScore> FinancialScores { get; set; }
        public DbSet<ScoreDetail> ScoreDetails { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
