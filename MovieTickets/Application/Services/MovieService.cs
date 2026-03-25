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
            Movie movie = new Movie(0,title,duration);
           

            repository.AddMovie(movie);
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return repository.GetAllMovies();


        }

     }
}
