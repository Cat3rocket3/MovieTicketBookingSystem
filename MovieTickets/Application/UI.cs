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

        public UI(MovieService service)
        {
            movieService = service;
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

        // ================= MOVIES =================

        private void MovieMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("MOVIES");

                ShowMovies();

                Console.WriteLine();
                Console.WriteLine("1. Add movie");
                Console.WriteLine("2. Remove movie");
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
                Console.WriteLine($"ID: {movie.Id} | {movie.Name} | {movie.Duration} minutes");
            }
        }

        private void AddMovie()
        {
            PrintHeader("ADD MOVIE");

            Console.Write("Movie title: ");
            string title = Console.ReadLine();

            int duration = ReadInt("Duration in minutes: ");

            try
            {
                movieService.AddMovie(title, duration);
                PrintSuccess("Movie added.");
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

        // ================= HALLS =================

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
            IReadOnlyList<Hall> halls = movieService.GetAllHalls();

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
                movieService.AddHall(rows, columns);
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

            if (movieService.GetAllHalls().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Hall ID: ");

            try
            {
                movieService.RemoveHall(id);
                PrintSuccess("Hall removed.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        // ================= PROJECTIONS =================

        private void ProjectionMenu()
        {
            bool running = true;

            while (running)
            {
                PrintHeader("PROJECTIONS");

                ShowProjections();

                Console.WriteLine();
                Console.WriteLine("1. Add projection");
                Console.WriteLine("2. Remove projection");
                Console.WriteLine("3. Search projections");
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
                        RemoveProjection();
                        break;

                    case "3":
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
            IReadOnlyList<Projection> projections = movieService.GetAllProjections();

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

            if (movieService.GetAllHalls().Count == 0)
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
                movieService.AddProjection(movieId, hallId, price, date);
                PrintSuccess("Projection added.");
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

            if (movieService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int id = ReadInt("Projection ID: ");

            try
            {
                movieService.RemoveProjection(id);
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

            Console.Write("Movie name: ");
            string search = Console.ReadLine();

            if (search == null)
                search = "";

            search = search.ToLower();

            var results = movieService.GetAllProjections()
                .Where(p => p.Movie != null && p.Movie.Name.ToLower().Contains(search))
                .ToList();

            if (results.Count == 0)
            {
                PrintMuted("No projections found.");
                Pause();
                return;
            }

            foreach (Projection p in results)
            {
                Console.WriteLine(
                    $"ID: {p.Id} | Movie: {p.Movie.Name} | Hall: {p.HallId} | Price: {p.Price} lv | Date: {p.Date:yyyy-MM-dd HH:mm}"
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

            if (movieService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = movieService.GetAllProjections()
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

            if (movieService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = movieService.GetAllProjections()
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
                movieService.ReserveTicket(projectionId, seatNumber);
                PrintSuccess("Ticket reserved successfully.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void PayTicket()
        {
            PrintHeader("PAY TICKET");

            ShowAllTicketsShort();

            int ticketId = ReadInt("Ticket ID to pay: ");

            try
            {
                movieService.PayTicket(ticketId);
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
            PrintHeader("CANCEL RESERVATION");

            ShowAllTicketsShort();

            int ticketId = ReadInt("Ticket ID to cancel: ");

            try
            {
                movieService.CancelReservation(ticketId);
                PrintSuccess("Reservation cancelled.");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Pause();
        }

        private void PrintTicketText()
        {
            PrintHeader("GENERATE TICKET");

            ShowAllTicketsShort();

            int ticketId = ReadInt("Ticket ID: ");

            try
            {
                string text = movieService.GenerateTicketText(ticketId);

                Console.WriteLine();
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
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
                .OrderBy(t => t.Seat.Row)
                .ThenBy(t => t.Seat.Column)
                .GroupBy(t => t.Seat.Row);

            foreach (var row in rows)
            {
                Console.Write($"Row {row.Key}: ");

                foreach (Ticket ticket in row)
                {
                    string status = movieService.GetTicketStatus(ticket);

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
            IReadOnlyList<Ticket> tickets = movieService.GetAllTickets();

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

                string status = movieService.GetTicketStatus(ticket);

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

            if (movieService.GetAllProjections().Count == 0)
            {
                Pause();
                return;
            }

            int projectionId = ReadInt("Projection ID: ");

            Projection projection = movieService.GetAllProjections()
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

            decimal revenue = movieService.GetAllTickets()
                .Where(t => t.IsPaid && !t.IsCancelled)
                .Sum(t => t.Price);

            Console.WriteLine($"Total revenue: {revenue} lv");

            Pause();
        }

        private void MostWatchedMoviesReport()
        {
            PrintHeader("MOST WATCHED MOVIES");

            var report = movieService.GetAllTickets()
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
        //mazna

    }
}