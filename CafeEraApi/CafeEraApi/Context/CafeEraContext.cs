using CafeEraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeEraApi.Context
{
    public class CafeEraContext : DbContext
    {
        public CafeEraContext(DbContextOptions<CafeEraContext> options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();
        }
    }
}
