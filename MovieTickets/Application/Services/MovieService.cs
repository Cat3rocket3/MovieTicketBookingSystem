using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using MovieTickets.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace MovieTickets.Application.Services
{
    public class MovieService
    {
        private readonly ITheaterRepository repository;

        public object TicketStatus { get; private set; }

        public MovieService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public void BuyTicket(int projectionId, int seatId)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.SeatId == seatId);

            if (ticket == null)
                throw new Exception("Seat not found");

            if (ticket.IsSold)
                throw new Exception("Seat already sold");

            ticket.IsSold = true;

            repository.UpdateTicket(ticket);
        }

        public void AddMovie(string title, int duration)
        {
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
            var halls = repository.GetAllHalls();
            return halls;
        }

        public void AddHall(int rows, int columns)
        {
            List<Seat> seats = new List<Seat>();
            int seatnumber = 0;
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    Seat seat = new Seat( row, col);
                    seatnumber++;
                   

                    seats.Add(seat);

                }
            }
            Hall hall = new Hall( seats);
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
            var movie = repository.GetMovieById(movieId);
            var hall = repository.GetHallById(hallId);

            if (movie == null)
                throw new Exception("Movie not found");

            if (hall == null)
                throw new Exception("Hall not found");

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
                    IsSold = false
                };

                projection.Tickets.Add(ticket);
            }

            repository.AddProjection(projection);
        }

        public void RemoveProjection(int id)
        {
            repository.RemoveProjection(id);
         
        }
    }
}
