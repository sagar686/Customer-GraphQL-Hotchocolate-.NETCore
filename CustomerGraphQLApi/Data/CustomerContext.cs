using Microsoft.EntityFrameworkCore;
using CustomerGraphQLApi.Models;

namespace CustomerGraphQLApi.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Customer>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        }
    }
}
