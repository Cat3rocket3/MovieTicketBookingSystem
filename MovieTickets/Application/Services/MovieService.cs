using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class MovieService
    {
        private readonly ITheaterRepository repository;

        public MovieService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

       

        public void AddMovie(string title, int duration)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Movie title cannot be empty.");

            if (duration <= 0)
                throw new Exception("Movie duration must be positive.");

            Movie movie = new Movie(0, title, duration);
            repository.AddMovie(movie);
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return repository.GetAllMovies();
        }

        public void RemoveMovie(int id)
        {
            repository.RemoveMovie(id);
        }

       

        public IReadOnlyList<Hall> GetAllHalls()
        {
            return repository.GetAllHalls();
        }

        public void AddHall(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new Exception("Rows and columns must be positive.");

            List<Seat> seats = new List<Seat>();

            int seatNumber = 1;

            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= columns; column++)
                {
                    Seat seat = new Seat(row, column, seatNumber);
                    seats.Add(seat);

                    seatNumber++;
                }
            }

            Hall hall = new Hall(seats);
            repository.AddHall(hall);
        }

        public void RemoveHall(int id)
        {
            repository.RemoveHall(id);
        }

       

        public IReadOnlyList<Projection> GetAllProjections()
        {
            return repository.GetAllProjections();
        }

        public void AddProjection(int movieId, int hallId, decimal price, DateTime date)
        {
            Movie movie = repository.GetMovieById(movieId);
            Hall hall = repository.GetHallById(hallId);

            if (movie == null)
                throw new Exception("Movie not found.");

            if (hall == null)
                throw new Exception("Hall not found.");

            if (price <= 0)
                throw new Exception("Price must be positive.");

            if (HasScheduleConflict(hallId, date, movie.Duration))
                throw new Exception("This hall already has a projection during this time.");

            Projection projection = new Projection
            {
                MovieId = movieId,
                HallId = hallId,
                Price = price,
                Date = date,
                Movie = movie,
                Hall = hall,
                Tickets = new List<Ticket>()
            };

            foreach (Seat seat in hall.Seats)
            {
                Ticket ticket = new Ticket
                {
                    Seat = seat,
                    SeatId = seat.Id,
                    Price = price,
                    IsReserved = false,
                    IsPaid = false,
                    IsCancelled = false
                };

                projection.Tickets.Add(ticket);
            }

            repository.AddProjection(projection);
        }

        private bool HasScheduleConflict(int hallId, DateTime newStart, int newMovieDuration)
        {
            DateTime newEnd = newStart.AddMinutes(newMovieDuration);

            var projections = repository.GetAllProjections()
                .Where(p => p.HallId == hallId)
                .ToList();

            foreach (Projection projection in projections)
            {
                if (projection.Movie == null)
                    continue;

                DateTime existingStart = projection.Date;
                DateTime existingEnd = projection.Date.AddMinutes(projection.Movie.Duration);

                bool overlaps = newStart < existingEnd && newEnd > existingStart;

                if (overlaps)
                    return true;
            }

            return false;
        }

        public void RemoveProjection(int id)
        {
            repository.RemoveProjection(id);
        }

        

        public IReadOnlyList<Ticket> GetAllTickets()
        {
            return repository.GetAllTickets();
        }

        public Ticket GetTicketById(int id)
        {
            return repository.GetTicketById(id);
        }

        public void ReserveTicket(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat != null && t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsPaid)
                throw new Exception("Seat is already paid.");

            if (ticket.IsReserved && !ticket.IsCancelled)
                throw new Exception("Seat is already reserved.");

            ticket.IsReserved = true;
            ticket.IsPaid = false;
            ticket.IsCancelled = false;

            repository.UpdateTicket(ticket);
        }

        public void PayTicket(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsCancelled)
                throw new Exception("Ticket is cancelled.");

            if (!ticket.IsReserved)
                throw new Exception("Ticket must be reserved first.");

            if (ticket.IsPaid)
                throw new Exception("Ticket already paid.");

            ticket.IsPaid = true;

            repository.UpdateTicket(ticket);
        }

        public void CancelReservation(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsPaid)
                throw new Exception("Paid ticket cannot be cancelled.");

            if (!ticket.IsReserved)
                throw new Exception("Ticket is not reserved.");

            ticket.IsReserved = false;
            ticket.IsPaid = false;
            ticket.IsCancelled = false; // important

            repository.UpdateTicket(ticket);
        }



        public string GenerateTicketText(int ticketId)
        {
            Ticket ticket = repository.GetTicketById(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found.");

            string movieName = ticket.Projection == null || ticket.Projection.Movie == null
                ? "N/A"
                : ticket.Projection.Movie.Name;

            string hall = ticket.Projection == null
                ? "N/A"
                : ticket.Projection.HallId.ToString();

            string date = ticket.Projection == null
                ? "N/A"
                : ticket.Projection.Date.ToString("yyyy-MM-dd HH:mm");

            return
                "========== MOVIE TICKET ==========\n" +
                $"Ticket ID: {ticket.Id}\n" +
                $"Movie: {movieName}\n" +
                $"Hall: {hall}\n" +
                $"Row: {ticket.Seat.Row}, Seat: {ticket.Seat.Column}\n" +
                $"Date: {date}\n" +
                $"Price: {ticket.Price} lv\n" +
                $"Status: {GetTicketStatus(ticket)}\n" +
                "==================================";
        }

        public string GetTicketStatus(Ticket ticket)
        {
            if (ticket.IsCancelled)
                return "CANCELLED";

            if (ticket.IsPaid)
                return "PAID";

            if (ticket.IsReserved)
                return "RESERVED";

            return "FREE";
        }


        public string GenerateTicketText(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat != null && t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            return GenerateTicketText(ticket.Id);
        }
    }
}