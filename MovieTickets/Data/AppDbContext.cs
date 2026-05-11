using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain.Entities;

namespace MovieTickets.Data
{
    public partial class AppDbContext : DbContext
    {
        // 1. Parameterless constructor (Crucial for Design-Time tools)
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Projection> Projections { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        // 2. OnConfiguring allows the 'Add-Migration' command to find the DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Update this string if your local SQL Server instance is different
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=MovieTicketsDb;User ID=sa;Password=144g144gG@;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship Configurations
            modelBuilder.Entity<Projection>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Projections)
                    .HasForeignKey(d => d.MovieId);

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Projections)
                    .HasForeignKey(d => d.HallId);
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.HallId);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Projection)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ProjectionId);

                entity.HasOne(d => d.Seat)
                    .WithMany()
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}