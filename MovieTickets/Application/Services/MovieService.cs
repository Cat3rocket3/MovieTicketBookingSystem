using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using MovieTickets.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    Seat seat = new Seat(0, row, col);
                    repository.AddSeat(seat);
                  //  seats.Add(repository.AddSeat(seat));
                }
            }
            Hall hall = new Hall(0, seats);
            repository.AddHall(hall);
        }

        public void RemoveHall(int id)
        {
            repository.RemoveHall(id);
        }
    }
}
