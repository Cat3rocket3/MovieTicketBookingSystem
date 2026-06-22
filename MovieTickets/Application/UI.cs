using MovieTickets.Application.Services;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTickets.Application
{
    internal class UI
    {
        private readonly MovieService movieService;
        private readonly HallService hallService;
        private readonly ProjectionService projectionService;
        private readonly TicketService ticketService;
        private readonly ReportService reportService;
        private readonly GenreService genreService;
        private readonly UserService userService;


        public UI(
            MovieService movieService,
            HallService hallService,
            ProjectionService projectionService,
            TicketService ticketService,
            ReportService reportService,
            GenreService genreService,
            UserService userService)
        {
            this.movieService = movieService;
            this.hallService = hallService;
            this.projectionService = projectionService;
            this.ticketService = ticketService;
            this.reportService = reportService;
            this.genreService = genreService;
            this.userService = userService;

            Console.OutputEncoding = Encoding.UTF8;
        }

        public void ShowMainMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("MOVIE TICKET BOOKING SYSTEM");

                Console.WriteLine("1. Movies");
                Console.WriteLine("2. Halls");
                Console.WriteLine("3. Projections");
                Console.WriteLine("4. Tickets");
                Console.WriteLine("5. Reports");
                Console.WriteLine("6. Genres");
                Console.WriteLine("7. Users");
                Console.WriteLine("0. Exit");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MovieMenu();
                        break;

                    case "2":
                        HallMenu();
                        break;

                    case "3":
                        ProjectionMenu();
                        break;

                    case "4":
                        TicketMenu();
                        break;

                    case "5":
                        ReportsMenu();
                        break;

                    case "6":
                        GenreMenu();
                        break;

                    case "7":
                        UserMenu();
                        break;

                    case "0":
                        PrintSuccess("Goodbye!");
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        

        private void MovieMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("MOVIES");

                ShowMovies();

                Console.WriteLine();
                Console.WriteLine("1. Add movie");
                Console.WriteLine("2. Edit movie");
                Console.WriteLine("3. Remove movie");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddMovie();
                        break;

                    case "2":
                        EditMovie();
                        break;

                    case "3":
                        RemoveMovie();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowMovies()
        {
            IReadOnlyList<Movie> movies = movieService.GetAllMovies();

            if (movies.Count == 0)
            {
                PrintMuted("No movies at this moment.");
                return;
            }

            foreach (Movie movie in movies)
            {
                string genreName = movie.Genre == null ? "N/A" : movie.Genre.Name;
                Console.WriteLine($"ID: {movie.Id} | {movie.Name} | {movie.Duration} minutes | Genre: {genreName}");
            }
        }

        private void AddMovie()
        {
            PrintHeader("ADD MOVIE");

            Console.Write("Movie title: ");
            string title = Console.ReadLine();

            int duration = ReadInt("Duration in minutes: ");

            int? genreId = ChooseGenreOptional();

            try
            {
                movieService.AddMovie(title, duration, genreId);
                PrintSuccess("Movie added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void EditMovie()
        {
            PrintHeader("EDIT MOVIE");

            ShowMovies();

            if (movieService.GetAllMovies().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Movie ID: ");

            Movie movie = movieService.GetMovieById(id);

            if (movie == null)
            {
                PrintError("Movie not found.");
                Pause();
                return;
            }

            Console.Write($"New title (leave empty to keep '{movie.Name}'): ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                title = movie.Name;

            Console.Write($"New duration in minutes (leave empty to keep {movie.Duration}): ");
            string durationInput = Console.ReadLine();
            int duration = string.IsNullOrWhiteSpace(durationInput)
                ? movie.Duration
                : int.Parse(durationInput);

            int? genreId = ChooseGenreOptional();

            try
            {
                movieService.EditMovie(id, title, duration, genreId);
                PrintSuccess("Movie updated.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveMovie()
        {
            PrintHeader("REMOVE MOVIE");

            ShowMovies();

            if (movieService.GetAllMovies().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Movie ID: ");

            try
            {
                movieService.RemoveMovie(id);
                PrintSuccess("Movie removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

       

        private void GenreMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("GENRES");

                ShowGenres();

                Console.WriteLine();
                Console.WriteLine("1. Add genre");
                Console.WriteLine("2. Remove genre");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGenre();
                        break;

                    case "2":
                        RemoveGenre();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowGenres()
        {
            IReadOnlyList<Genre> genres = genreService.GetAllGenres();

            if (genres.Count == 0)
            {
                PrintMuted("No genres at this moment.");
                return;
            }

            foreach (Genre genre in genres)
            {
                Console.WriteLine($"ID: {genre.Id} | {genre.Name}");
            }
        }

        private void AddGenre()
        {
            PrintHeader("ADD GENRE");

            Console.Write("Genre name: ");
            string name = Console.ReadLine();

            try
            {
                genreService.AddGenre(name);
                PrintSuccess("Genre added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveGenre()
        {
            PrintHeader("REMOVE GENRE");

            ShowGenres();

            if (genreService.GetAllGenres().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Genre ID: ");

            try
            {
                genreService.RemoveGenre(id);
                PrintSuccess("Genre removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        
        private int? ChooseGenreOptional()
        {
            IReadOnlyList<Genre> genres = genreService.GetAllGenres();

            if (genres.Count == 0)
            {
                PrintMuted("No genres defined yet (you can add one from the Genres menu).");
                return null;
            }

            Console.WriteLine("Available genres:");
            foreach (Genre genre in genres)
            {
                Console.WriteLine($"  ID: {genre.Id} | {genre.Name}");
            }

            Console.Write("Genre ID (leave empty for none): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (int.TryParse(input, out int genreId))
                return genreId;

            return null;
        }

       

        private void UserMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("USERS");

                ShowUsers();

                Console.WriteLine();
                Console.WriteLine("1. Add user");
                Console.WriteLine("2. Remove user");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;

                    case "2":
                        RemoveUser();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowUsers()
        {
            IReadOnlyList<User> users = userService.GetAllUsers();

            if (users.Count == 0)
            {
                PrintMuted("No users at this moment.");
                return;
            }

            foreach (User user in users)
            {
                Console.WriteLine($"ID: {user.Id} | {user.FullName} | {user.Email}");
            }
        }

        private void AddUser()
        {
            PrintHeader("ADD USER");

            Console.Write("Full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            try
            {
                userService.AddUser(fullName, email);
                PrintSuccess("User added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveUser()
        {
            PrintHeader("REMOVE USER");

            ShowUsers();

            if (userService.GetAllUsers().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("User ID: ");

            try
            {
                userService.RemoveUser(id);
                PrintSuccess("User removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

     

        private void HallMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("HALLS");

                ShowHalls();

                Console.WriteLine();
                Console.WriteLine("1. Add hall");
                Console.WriteLine("2. Remove hall");
                Console.WriteLine("3. Manage seats");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddHall();
                        break;

                    case "2":
                        RemoveHall();
                        break;

                    case "3":
                        SeatMenu();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowHalls()
        {
            IReadOnlyList<Hall> halls = hallService.GetAllHalls();

            if (halls.Count == 0)
            {
                PrintMuted("No halls at this moment.");
                return;
            }

            foreach (Hall hall in halls)
            {
                int capacity = hall.Seats == null ? 0 : hall.Seats.Count;
                Console.WriteLine($"ID: {hall.Id} | Capacity: {capacity} seats");
            }
        }

        private void AddHall()
        {
            PrintHeader("ADD HALL");

            int rows = ReadInt("Rows: ");
            int columns = ReadInt("Columns: ");

            try
            {
                hallService.AddHall(rows, columns);
                PrintSuccess("Hall added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveHall()
        {
            PrintHeader("REMOVE HALL");

            ShowHalls();

            if (hallService.GetAllHalls().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Hall ID: ");

            try
            {
                hallService.RemoveHall(id);
                PrintSuccess("Hall removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }



        private void SeatMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("MANAGE SEATS");

                ShowHalls();

                if (hallService.GetAllHalls().Count == 0)
                {
                    Pause();
                    return;
                }

                int hallId = ReadInt("Select Hall ID to view/edit seats: ");

                Hall hall = hallService.GetHallById(hallId);

                if (hall == null)
                {
                    PrintError("Hall not found.");
                    Pause();
                    continue;
                }

                ShowHallSeats(hall);

                Console.WriteLine();
                Console.WriteLine("1. Add seat to hall");
                Console.WriteLine("2. Remove seat");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSeat(hall.Id);
                        break;

                    case "2":
                        RemoveSeat(hall.Id);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }


        private void AddSeat(int hallId)
        {
            PrintHeader("ADD SEAT");

            int row = ReadInt("Row: ");
            int column = ReadInt("Column: ");

            try
            {
                hallService.AddSeat(hallId, row, column);
                PrintSuccess("Seat added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveSeat(int hallId)
        {
            PrintHeader("REMOVE SEAT");

            Hall hall = hallService.GetHallById(hallId);

            if (hall == null)
            {
                PrintError("Hall not found.");
                Pause();
                return;
            }

            if (hall.Seats == null || hall.Seats.Count == 0)
            {
                PrintMuted("This hall has no seats.");
                Pause();
                return;
            }

            Console.WriteLine($"Hall ID: {hall.Id}");
            PrintLine();

            foreach (Seat seat in hall.Seats)
            {
                Console.WriteLine(
                    $"Seat ID: {seat.Id} | Row {seat.Row}, Column {seat.Column} | Number {seat.Number}"
                );
            }

            PrintLine();

            int seatId = ReadInt("Seat ID to remove: ");

            try
            {
                hallService.RemoveSeat(seatId);
                PrintSuccess("Seat removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }



        private void ProjectionMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("PROJECTIONS");

                ShowProjections();

                Console.WriteLine();
                Console.WriteLine("1. Add projection");
                Console.WriteLine("2. Edit projection");
                Console.WriteLine("3. Remove projection");
                Console.WriteLine("4. Search projections");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProjection();
                        break;

                    case "2":
                        EditProjection();
                        break;

                    case "3":
                        RemoveProjection();
                        break;

                    case "4":
                        SearchProjections();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowProjections()
        {
            IReadOnlyList<Projection> projections = projectionService.GetAllProjections();

            if (projections.Count == 0)
            {
                PrintMuted("No projections at this moment.");
                return;
            }

            foreach (Projection p in projections)
            {
                string movieName = p.Movie == null ? "N/A" : p.Movie.Name;

                Console.WriteLine(
                    $"ID: {p.Id} | Movie: {movieName} | Hall: {p.HallId} | Price: {p.Price} lv | Date: {p.Date:yyyy-MM-dd HH:mm}"
                );
            }
        }

        private void AddProjection()
        {
            PrintHeader("ADD PROJECTION");

            if (movieService.GetAllMovies().Count == 0)
            {
                PrintError("You need to add a movie first.");
                Pause();
                return;
            }

            if (hallService.GetAllHalls().Count == 0)
            {
                PrintError("You need to add a hall first.");
                Pause();
                return;
            }

            Console.WriteLine("Available movies:");
            ShowMovies();

            int movieId = ReadInt("Movie ID: ");

            Console.WriteLine();
            Console.WriteLine("Available halls:");
            ShowHalls();

            int hallId = ReadInt("Hall ID: ");
            decimal price = ReadDecimal("Ticket price: ");
            DateTime date = ReadDateTime("Date (yyyy-MM-dd HH:mm): ");

            try
            {
                projectionService.AddProjection(movieId, hallId, price, date);
                PrintSuccess("Projection added.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void EditProjection()
        {
            PrintHeader("EDIT PROJECTION");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == id);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            Console.WriteLine("Available movies:");
            ShowMovies();
            int movieId = ReadInt($"Movie ID (currently {projection.MovieId}): ");

            Console.WriteLine("Available halls:");
            ShowHalls();
            int hallId = ReadInt($"Hall ID (currently {projection.HallId}): ");

            decimal price = ReadDecimal($"Ticket price (currently {projection.Price}): ");
            DateTime date = ReadDateTime("New date (yyyy-MM-dd HH:mm): ");

            try
            {
                projectionService.EditProjection(id, movieId, hallId, price, date);
                PrintSuccess("Projection updated.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void RemoveProjection()
        {
            PrintHeader("REMOVE PROJECTION");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Projection ID: ");

            try
            {
                projectionService.RemoveProjection(id);
                PrintSuccess("Projection removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void SearchProjections()
        {
            PrintHeader("SEARCH PROJECTIONS");

            Console.WriteLine("Available movies:");
            ShowMovies();
            Console.WriteLine();

            int movieId = ReadInt("Movie ID: ");

            var results = projectionService.GetAllProjections()
                .Where(p => p.MovieId == movieId)
                .ToList();

            if (results.Count == 0)
            {
                PrintMuted("No projections found.");
                Pause();
                return;
            }

            foreach (Projection p in results)
            {
                string movieName = p.Movie == null ? "N/A" : p.Movie.Name;

                Console.WriteLine(
                    $"ID: {p.Id} | Movie: {movieName} | Hall: {p.HallId} | Price: {p.Price} lv | Date: {p.Date:yyyy-MM-dd HH:mm}"
                );
            }

            Pause();
        }


        private void TicketMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("TICKETS");

                Console.WriteLine("1. Show seats for projection");
                Console.WriteLine("2. Reserve ticket");
                Console.WriteLine("3. Pay ticket");
                Console.WriteLine("4. Cancel reservation");
                Console.WriteLine("5. Generate ticket text");
                Console.WriteLine("6. Booking history (by user)");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowSeatsForProjection();
                        break;

                    case "2":
                        ReserveTicket();
                        break;

                    case "3":
                        PayTicket();
                        break;

                    case "4":
                        CancelReservation();
                        break;

                    case "5":
                        PrintTicketText();
                        break;

                    case "6":
                        BookingHistory();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void ShowSeatsForProjection()
        {
            PrintHeader("SEATS");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            PrintSeats(projection);

            Pause();
        }

        private void ReserveTicket()
        {
            Console.Clear();
            PrintTitle("RESERVE TICKET");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            PrintSeats(projection);

            int seatNumber = ReadInt("Seat number: ");

            int? userId = ChooseUserOptional();

            try
            {
                ticketService.ReserveTicket(projectionId, seatNumber, userId);
                PrintSuccess("Ticket reserved successfully.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

      
        private int? ChooseUserOptional()
        {
            IReadOnlyList<User> users = userService.GetAllUsers();

            if (users.Count == 0)
            {
                PrintMuted("No users registered yet (you can add one from the Users menu).");
                return null;
            }

            Console.WriteLine("Registered users:");
            foreach (User user in users)
            {
                Console.WriteLine($"  ID: {user.Id} | {user.FullName}");
            }

            Console.Write("User ID (leave empty for guest): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (int.TryParse(input, out int userId))
                return userId;

            return null;
        }

        private void PayTicket()
        {
            Console.Clear();
            PrintTitle("PAY TICKET");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            PrintSeats(projection);

            int seatNumber = ReadInt("Seat number: ");

            try
            {
                ticketService.PayTicket(projectionId, seatNumber);
                PrintSuccess("Ticket paid.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void CancelReservation()
        {
            Console.Clear();
            PrintTitle("CANCEL TICKET");

            ShowProjections();

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            PrintSeats(projection);

            int seatNumber = ReadInt("Seat number: ");

            try
            {
                ticketService.CancelReservation(projectionId, seatNumber);
                PrintSuccess("Ticket cancelled.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void PrintTicketText()
        {
            Console.Clear();
            PrintTitle("GENERATE TICKET");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            PrintSeats(projection);

            int seatNumber = ReadInt("Seat number: ");

            try
            {
                string text = reportService.GenerateTicketText(projectionId, seatNumber);

                Console.WriteLine();
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void BookingHistory()
        {
            Console.Clear();
            PrintTitle("BOOKING HISTORY");

            ShowUsers();

            if (userService.GetAllUsers().Count == 0)
            {
                Pause();
                return;
            }

            int userId = ReadInt("User ID: ");

            IReadOnlyList<Ticket> tickets = ticketService.GetBookingHistory(userId);

            if (tickets.Count == 0)
            {
                PrintMuted("No bookings found for this user.");
                Pause();
                return;
            }

            foreach (Ticket ticket in tickets)
            {
                string movieName = ticket.Projection == null || ticket.Projection.Movie == null
                    ? "N/A"
                    : ticket.Projection.Movie.Name;

                string status = reportService.GetTicketStatus(ticket);
                string seatInfo = ticket.Seat == null ? "N/A" : $"Row {ticket.Seat.Row}, Seat {ticket.Seat.Column}";

                Console.WriteLine(
                    $"Ticket ID: {ticket.Id} | Movie: {movieName} | Seat: {seatInfo} | Price: {ticket.Price} lv | Status: {status} | Booked: {ticket.CreatedAt:yyyy-MM-dd HH:mm}"
                );
            }

            Pause();
        }

        private void PrintSeats(Projection projection)
        {
            if (projection.Tickets == null || projection.Tickets.Count == 0)
            {
                PrintMuted("This projection has no tickets.");
                return;
            }

            string movieName = projection.Movie == null ? "N/A" : projection.Movie.Name;

            Console.WriteLine();
            Console.WriteLine($"Movie: {movieName}");
            Console.WriteLine($"Hall: {projection.HallId}");
            Console.WriteLine($"Date: {projection.Date:yyyy-MM-dd HH:mm}");
            PrintLine();

            var rows = projection.Tickets
                .Where(t => t.Seat != null)
                .OrderBy(t => t.Seat.Number)
                .ThenBy(t => t.Seat.Row)
                .ThenBy(t => t.Seat.Column)
                .GroupBy(t => t.Seat.Row);


           

            foreach (var row in rows)
            {
                Console.Write($"Row {row.Key}: ");

                foreach (Ticket ticket in row)
                {
                    string status = reportService.GetTicketStatus(ticket);

                    if (status == "FREE")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (status == "RESERVED")
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (status == "PAID")
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"[{ticket.Seat.Number}-{status}] ");

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            PrintLine();
            Console.WriteLine("Choose seat by seat number.");
        }

        private void ShowAllTicketsShort()
        {
            IReadOnlyList<Ticket> tickets = ticketService.GetAllTickets();

            if (tickets.Count == 0)
            {
                PrintMuted("No tickets yet.");
                return;
            }

            foreach (Ticket ticket in tickets)
            {
                string movieName = ticket.Projection == null || ticket.Projection.Movie == null
                    ? "N/A"
                    : ticket.Projection.Movie.Name;

                string status = reportService.GetTicketStatus(ticket);

                Console.WriteLine(
                    $"Ticket ID: {ticket.Id} | Movie: {movieName} | Seat: Row {ticket.Seat.Row}, Seat {ticket.Seat.Column} | Price: {ticket.Price} lv | Status: {status}"
                );
            }
        }


        private void ReportsMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("REPORTS");

                Console.WriteLine("1. Hall occupancy by projection");
                Console.WriteLine("2. Revenue report");
                Console.WriteLine("3. Most watched movies");
                Console.WriteLine("0. Back");
                PrintLine();

                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HallOccupancyReport();
                        break;

                    case "2":
                        RevenueReport();
                        break;

                    case "3":
                        MostWatchedMoviesReport();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        PrintError("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void HallOccupancyReport()
        {
            PrintHeader("HALL OCCUPANCY");

            ShowProjections();

            if (projectionService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = projectionService.GetAllProjections()
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                PrintError("Projection not found.");
                Pause();
                return;
            }

            int totalSeats = projection.Tickets.Count;
            int occupiedSeats = projection.Tickets.Count(t => (t.IsReserved || t.IsPaid) && !t.IsCancelled);
            int freeSeats = totalSeats - occupiedSeats;

            string movieName = projection.Movie == null ? "N/A" : projection.Movie.Name;

            Console.WriteLine();
            Console.WriteLine($"Movie: {movieName}");
            Console.WriteLine($"Hall: {projection.HallId}");
            Console.WriteLine($"Total seats: {totalSeats}");
            Console.WriteLine($"Occupied seats: {occupiedSeats}");
            Console.WriteLine($"Free seats: {freeSeats}");

            Pause();
        }

        private void RevenueReport()
        {
            PrintHeader("REVENUE REPORT");

            decimal revenue = ticketService.GetAllTickets()
                .Where(t => t.IsPaid && !t.IsCancelled)
                .Sum(t => t.Price);

            Console.WriteLine($"Total revenue: {revenue} lv");

            Pause();
        }

        private void MostWatchedMoviesReport()
        {
            PrintHeader("MOST WATCHED MOVIES");

            var report = ticketService.GetAllTickets()
                .Where(t => t.IsPaid && !t.IsCancelled)
                .Where(t => t.Projection != null && t.Projection.Movie != null)
                .GroupBy(t => t.Projection.Movie.Name)
                .Select(g => new
                {
                    MovieName = g.Key,
                    TicketsSold = g.Count()
                })
                .OrderByDescending(x => x.TicketsSold)
                .ToList();

            if (report.Count == 0)
            {
                PrintMuted("No paid tickets yet.");
                Pause();
                return;
            }

            foreach (var item in report)
            {
                Console.WriteLine($"{item.MovieName} - {item.TicketsSold} paid tickets");
            }

            Pause();
        }



        private int ReadInt(string message)
        {
            int number;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out number))
                    return number;

                PrintError("Invalid number.");
            }
        }

        private decimal ReadDecimal(string message)
        {
            decimal number;

            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(Console.ReadLine(), out number))
                    return number;

                PrintError("Invalid price.");
            }
        }

        private void ShowHallSeats(Hall hall)
        {
            if (hall == null || hall.Seats == null || hall.Seats.Count == 0)
            {
                PrintMuted("This hall has no seats.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Hall ID: {hall.Id} - Seat Layout");
            PrintLine();


            var grouped = hall.Seats
             .OrderBy(s => s.Number)
             .ThenBy(s => s.Column)
             .GroupBy(s => s.Row);
           

            foreach (var row in grouped)
            {
                Console.Write($"Row {row.Key}: ");

                foreach (var seat in row)
                {
                    Console.Write($"[{seat.Number}] ");
                }

                Console.WriteLine();
            }

            PrintLine();
        }

        private DateTime ReadDateTime(string message)
        {
            DateTime date;

            while (true)
            {
                Console.Write(message);

                if (DateTime.TryParse(Console.ReadLine(), out date))
                    return date;

                PrintError("Invalid date. Use format: yyyy-MM-dd HH:mm");
            }
        }

        private void PrintHeader(string title)
        {
            Console.WriteLine();
            PrintLine();
            Console.WriteLine(title);
            PrintLine();
        }

        private void PrintLine()
        {
            Console.WriteLine("----------------------------------------");
        }

        private void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void PrintMuted(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        private void PrintTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintLine();
            Console.WriteLine(title);
            PrintLine();
            Console.ResetColor();
        }

    }
}