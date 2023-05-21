using Microsoft.EntityFrameworkCore;
using CardCollectorsCompiler.Models;
using Microsoft.CodeAnalysis;

namespace CardCollectorsCompiler.Services
{
    public class CCCDbContext : DbContext { 

        public CCCDbContext()
        {

        }
        public CCCDbContext(DbContextOptions<CCCDbContext> options) : base(options) {

        }

        public DbSet<Set> Sets { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Card> Cards { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CCCcontext");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Set>().ToTable("Sets");
            modelBuilder.Entity<Language>().ToTable("Languages");
            modelBuilder.Entity<Card>().ToTable("Cards");
        }
    }
}
