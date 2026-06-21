using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain.Entities;

namespace MovieTickets.Data
{
    public partial class AppDbContext : DbContext
    {
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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=192.168.1.8,1433;Database=MovieTickets67;Initial Catalog=MovieTicketsDb;User ID=sa;Password=144g144gG@;Encrypt=True;TrustServerCertificate=True");
            }
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //            "Server=K207\\SQLEXPRESS;Database=MovieTickets;Trusted_Connection=True;TrustServerCertificate=True"
        //        );
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasOne(m => m.Genre)
                    .WithMany(g => g.Movies)
                    .HasForeignKey(m => m.GenreId);
            });

            modelBuilder.Entity<Projection>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(p => p.Movie)
                    .WithMany(m => m.Projections)
                    .HasForeignKey(p => p.MovieId);

                entity.HasOne(p => p.Hall)
                    .WithMany(h => h.Projections)
                    .HasForeignKey(p => p.HallId);
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasOne(s => s.Hall)
                    .WithMany(h => h.Seats)
                    .HasForeignKey(s => s.HallId);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(t => t.Projection)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(t => t.ProjectionId);

                entity.HasOne(t => t.Seat)
                    .WithMany()
                    .HasForeignKey(t => t.SeatId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}