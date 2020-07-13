using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pai.Areas.Identity.Data;
using Pai.DatabaseModels;

namespace Pai.Data
{
    public class ApplicationDbContext : IdentityDbContext<PaiUser>
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sponsor> Sponsor { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<TournamentUser> TournamentUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:pai-db.database.windows.net,1433;Initial Catalog=pai-db;Persist Security Info=False;User ID=pai-admin;Password=P4i-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Sponsor)
                    .HasForeignKey(d => d.TournamentId);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TournamentUser>(entity =>
            {
                entity.HasIndex(e => e.LicenceNumber)
                    .HasName("UQ__Tourname__1EF805903C88AF5B")
                    .IsUnique();

                entity.HasIndex(e => e.RankNumber)
                    .HasName("UQ__Tourname__02EB88A5C7EBF122")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LicenceNumber)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentUser)
                    .HasForeignKey(d => d.TournamentId);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TournamentUser>()
                .HasOne(po => po.IdentityUser)
                .WithMany(a => a.TournamentUser)
                .HasForeignKey(po => po.UserId);
        }
    }
}
