using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MovieTickets.Application.Services
{
    public class GenreService
    {
        private readonly ITheaterRepository repository;

        public GenreService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<Genre> GetAllGenres()
        {
            return repository.GetAllGenres();
        }

        public void AddGenre(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Genre name cannot be empty.");

            Genre genre = new Genre { Name = name };
            repository.AddGenre(genre);
        }

        public void RemoveGenre(int id)
        {
            repository.RemoveGenre(id);
        }
    }
}