using Microsoft.EntityFrameworkCore;
using Pai.Models;

namespace Pai.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>().ToTable("Tournament");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsor");
        }
    }
}
