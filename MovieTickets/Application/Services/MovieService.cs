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

        public Movie GetMovieById(int id)
        {
            return repository.GetMovieById(id);
        }
        public IReadOnlyList<Movie> GetAllMovies()
        {
            return repository.GetAllMovies();


        }
        public void RemoveMovie(int id)
        {
            Movie movie = repository.GetMovieById(id);
            if (movie != null)
            {
                repository.RemoveMovie(id);
            }
            else
            {
                throw new Exception("Movie not found");
            }
        }

        public void AddProjection(Movie movie, Hall hall, decimal price, DateTime date)
        {
            Projection projection = new Projection(movie, hall, price, date);
            repository.AddProjection(projection);
        }

        public Projection GetProjectionById(int id)
        {
            return repository.GetProjectionById(id);
        }
        public IReadOnlyList<Projection> GetAllProjections()
        {
            return repository.GetAllProjections();
        }
    }
}
