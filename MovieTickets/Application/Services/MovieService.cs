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

        public void AddMovie(string title, int duration, int? genreId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Movie title cannot be empty.");

            if (duration <= 0)
                throw new Exception("Movie duration must be positive.");

            Movie movie = new Movie(0, title, duration);
            movie.GenreId = genreId;

            repository.AddMovie(movie);
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return repository.GetAllMovies();
        }

        public Movie GetMovieById(int id)
        {
            return repository.GetMovieById(id);
        }

        public void EditMovie(int id, string title, int duration, int? genreId)
        {
            Movie movie = repository.GetMovieById(id);

            if (movie == null)
                throw new Exception("Movie not found.");

            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Movie title cannot be empty.");

            if (duration <= 0)
                throw new Exception("Movie duration must be positive.");

            movie.Name = title;
            movie.Duration = duration;
            movie.GenreId = genreId;

            repository.UpdateMovie(movie);
        }

        public void RemoveMovie(int id)
        {
            repository.RemoveMovie(id);
        }
    }
}