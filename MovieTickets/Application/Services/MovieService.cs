using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;

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
    }
}