using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieTickets.Application;
using MovieTickets.Application.interfaces;
using MovieTickets.Application.Services;
using MovieTickets.Data;
using MovieTickets.Domain.Entities;
using MovieTickets.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets
{
    internal class Program
    {

        static void Main(string[] args)
        {

            //var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=K207\\SQLEXPRESS;Database=MovieTickets;Integrated Security=True;").EnableSensitiveDataLogging().Options;


            var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer("Server=localhost,1433;Database=MovieTickets;Initial Catalog=MovieTicketsDb;User ID=sa;Password=144g144gG@;Encrypt=True;TrustServerCertificate=True")
    .EnableSensitiveDataLogging()
    .Options;

            var db = new AppDbContext(options);

            ITheaterRepository repository = new TheaterSqlRepository(db);

            var movieService = new MovieService(repository);
            var hallService = new HallService(repository);
            var projectionService = new ProjectionService(repository);
            var ticketService = new TicketService(repository);
            var reportService = new ReportService(repository);
            var genreService = new GenreService(repository);
            var userService = new UserService(repository);

            var ui = new UI( movieService, hallService,  projectionService, ticketService,   reportService,  genreService,  userService);
            SeedDatabase(db);
            ui.ShowMainMenu();
        }
        public static void SeedDatabase(AppDbContext context)
        {
            if (context == null) return;

            // Clear existing data (FK order matters)
            context.Tickets.RemoveRange(context.Tickets);
            context.Projections.RemoveRange(context.Projections);
            context.Seats.RemoveRange(context.Seats);
            context.Movies.RemoveRange(context.Movies);
            context.Users.RemoveRange(context.Users);
            context.Genres.RemoveRange(context.Genres);
            context.Halls.RemoveRange(context.Halls);
            context.SaveChanges();

            // ---------------- GENRES ----------------
            var genres = new List<Genre>
    {
        new Genre { Name = "Action" },
        new Genre { Name = "Drama" },
        new Genre { Name = "Comedy" },
        new Genre { Name = "Sci-Fi" },
        new Genre { Name = "Horror" },
        new Genre { Name = "Fantasy" },
        new Genre { Name = "Thriller" },
        new Genre { Name = "Animation" },
        new Genre { Name = "Adventure" },
        new Genre { Name = "Crime" },
        new Genre { Name = "Romance" },
        new Genre { Name = "Mystery" },
        new Genre { Name = "War" },
        new Genre { Name = "Western" },
        new Genre { Name = "Biography" },
        new Genre { Name = "Family" },
        new Genre { Name = "History" },
        new Genre { Name = "Musical" },
        new Genre { Name = "Sport" },
        new Genre { Name = "Documentary" }
    };

            context.Genres.AddRange(genres);

            // ---------------- USERS ----------------
            var users = new List<User>
    {
        new User { FullName = "Alex Johnson", Email = "alex.johnson@gmail.com" },
        new User { FullName = "Maria Petrova", Email = "maria.petrova@gmail.com" },
        new User { FullName = "Ivan Dimitrov", Email = "ivan.dimitrov@gmail.com" },
        new User { FullName = "Emily Carter", Email = "emily.carter@gmail.com" },
        new User { FullName = "John Smith", Email = "john.smith@gmail.com" },
        new User { FullName = "Sofia Ivanova", Email = "sofia.ivanova@gmail.com" },
        new User { FullName = "Daniel Brown", Email = "daniel.brown@gmail.com" },
        new User { FullName = "Nikolay Stoyanov", Email = "nikolay.stoyanov@gmail.com" },
        new User { FullName = "Laura Wilson", Email = "laura.wilson@gmail.com" },
        new User { FullName = "Peter Georgiev", Email = "peter.georgiev@gmail.com" },
        new User { FullName = "Chris Evans", Email = "chris.evans@gmail.com" },
        new User { FullName = "Natalie Wood", Email = "natalie.wood@gmail.com" },
        new User { FullName = "George Miller", Email = "george.miller@gmail.com" },
        new User { FullName = "Anna Keller", Email = "anna.keller@gmail.com" },
        new User { FullName = "Martin Lewis", Email = "martin.lewis@gmail.com" },
        new User { FullName = "Victoria Rose", Email = "victoria.rose@gmail.com" },
        new User { FullName = "Dimitar Hristov", Email = "dimitar.hristov@gmail.com" },
        new User { FullName = "Sophia Turner", Email = "sophia.turner@gmail.com" },
        new User { FullName = "Tom Hardy", Email = "tom.hardy@gmail.com" },
        new User { FullName = "Elena Markova", Email = "elena.markova@gmail.com" }
    };

            context.Users.AddRange(users);

            // ---------------- HALLS ----------------
            var halls = Enumerable.Range(1, 20)
                .Select(i => new Hall
                {
                    // optional if you have Name field
                    // Name = $"Hall {i}"
                })
                .ToList();

            context.Halls.AddRange(halls);

            context.SaveChanges();

            // ---------------- MOVIES ----------------
            var movies = new List<Movie>
    {
        new Movie { Name = "Inception", Duration = 148, GenreId = genres[3].Id },
        new Movie { Name = "The Dark Knight", Duration = 152, GenreId = genres[0].Id },
        new Movie { Name = "Interstellar", Duration = 169, GenreId = genres[3].Id },
        new Movie { Name = "Titanic", Duration = 195, GenreId = genres[10].Id },
        new Movie { Name = "Avengers: Endgame", Duration = 181, GenreId = genres[0].Id },
        new Movie { Name = "Joker", Duration = 122, GenreId = genres[9].Id },
        new Movie { Name = "Toy Story", Duration = 81, GenreId = genres[7].Id },
        new Movie { Name = "Gladiator", Duration = 155, GenreId = genres[12].Id },
        new Movie { Name = "The Matrix", Duration = 136, GenreId = genres[3].Id },
        new Movie { Name = "Forrest Gump", Duration = 142, GenreId = genres[1].Id },
        new Movie { Name = "The Godfather", Duration = 175, GenreId = genres[9].Id },
        new Movie { Name = "Pulp Fiction", Duration = 154, GenreId = genres[6].Id },
        new Movie { Name = "The Shawshank Redemption", Duration = 142, GenreId = genres[1].Id },
        new Movie { Name = "Mad Max: Fury Road", Duration = 120, GenreId = genres[0].Id },
        new Movie { Name = "Spider-Man: No Way Home", Duration = 148, GenreId = genres[0].Id },
        new Movie { Name = "Frozen", Duration = 102, GenreId = genres[7].Id },
        new Movie { Name = "The Lion King", Duration = 88, GenreId = genres[15].Id },
        new Movie { Name = "Dune", Duration = 155, GenreId = genres[5].Id },
        new Movie { Name = "The Batman", Duration = 176, GenreId = genres[0].Id },
        new Movie { Name = "Avatar", Duration = 162, GenreId = genres[8].Id }
    };

            context.Movies.AddRange(movies);
            context.SaveChanges();

            // ---------------- SEATS ----------------
            // Create multiple seats per hall (e.g. 25 seats per hall arranged 5x5)
            var seats = new List<Seat>();
            const int seatsPerHall = 25; // at least 10, using 25 as requested
            const int seatsPerRow = 5; // layout 5 columns

            for (int h = 0; h < halls.Count; h++)
            {
                for (int s = 0; s < seatsPerHall; s++)
                {
                    int row = (s / seatsPerRow) + 1;
                    int column = (s % seatsPerRow) + 1;
                    int number = s + 1;

                    seats.Add(new Seat
                    {
                        HallId = halls[h].Id,
                        Row = row,
                        Column = column,
                        Number = number
                    });
                }
            }

            context.Seats.AddRange(seats);
            context.SaveChanges();

            // ---------------- PROJECTIONS ----------------
            var projections = Enumerable.Range(1, 20)
                .Select(i => new Projection
                {
                    MovieId = movies[i - 1].Id,
                    HallId = halls[i - 1].Id,
                    Price = 8.00m + (i % 5)
                })
                .ToList();

            context.Projections.AddRange(projections);
            context.SaveChanges();

            // ---------------- TICKETS ----------------
            var tickets = Enumerable.Range(1, 20)
                .Select(i => new Ticket
                {
                    Price = projections[i - 1].Price,
                    ProjectionId = projections[i - 1].Id,
                    // pick the first seat of the corresponding hall (each hall has seatsPerHall seats)
                    SeatId = seats[(i - 1) * seatsPerHall].Id,
                    CreatedAt = DateTime.UtcNow.AddMinutes(-i * 10),
                    IsCancelled = false,
                    IsPaid = true,
                    IsReserved = false,
                    UserId = users[i - 1].Id
                })
                .ToList();

            context.Tickets.AddRange(tickets);
            context.SaveChanges();
        }
    }
}
