using Microsoft.EntityFrameworkCore;
using CardCollectorsCompiler.Models;

namespace CardCollectorsCompiler.Services
{
    public class CCCDbContext : DbContext { 

        public CCCDbContext()
        {

        }
        public CCCDbContext(DbContextOptions<CCCDbContext> options) : base(options) {

        }

        public DbSet<Set> Sets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CCCcontext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
