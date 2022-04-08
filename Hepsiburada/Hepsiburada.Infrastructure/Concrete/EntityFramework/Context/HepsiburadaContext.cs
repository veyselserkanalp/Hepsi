using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Hepsiburada.Infrastructure.Concrete.EntityFramework.Context
{
    public class HepsiburadaContext : DbContext
    {
        //DbSet<Test> Test { get; set; }


        public HepsiburadaContext(DbContextOptions<HepsiburadaContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Test>().ToTable("Tests");

        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HepsiburadaContext>
    {
        private readonly IConfiguration _configuration;
        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public HepsiburadaContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetSection("SqlServerSettings").GetSection("ConnectionString").Value;
            var builder = new DbContextOptionsBuilder<HepsiburadaContext>();
            builder.UseSqlServer(connectionString);
            return new HepsiburadaContext(builder.Options);
        }
    }
}
