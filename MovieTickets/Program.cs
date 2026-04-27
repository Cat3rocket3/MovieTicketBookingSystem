using MovieTickets.Application;
using MovieTickets.Application.Services;
using MovieTickets.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new TheaterStorage();
            var repository = new ITheaterRepository(storage);
            var movieService = new MovieService(repository);
            var ui = new UI(movieService);
            ui.ShowMainMenu();
        }
        //wggtgtgttgt
    }
}
