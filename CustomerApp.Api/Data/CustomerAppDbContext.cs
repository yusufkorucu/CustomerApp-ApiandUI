using CustomerApp.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Api.Data
{
    public class CustomerAppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public CustomerAppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
