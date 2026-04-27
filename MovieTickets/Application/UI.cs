using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTickets.Application.Services; 
using MovieTickets.Application.interfaces;
using MovieTickets.Infrastructure;
using System.Diagnostics;
using System.CodeDom;

namespace MovieTickets.Application
{
    internal class UI
    {
         private readonly MovieService movieService;

        public UI(MovieService service)
        {
            this.movieService = service;
        }


        public void ShowMainMenu()
        {
            



        




            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to the Movie Theater!");
                Console.WriteLine("1. View Movies");
                Console.WriteLine("2.Add Movie");
                Console.WriteLine("3.Remove Movie");
                Console.WriteLine("0. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowMovies();
                        break;
                    case "2":
                        AddMovie();
                        break;
                    case "3":
                        ShowMovies();
                        //remove
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        running = false;
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }

        public void ShowMovies()


             
        {
         var movies = movieService.GetAllMovies();
            Console.WriteLine("Movies:");
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Id}. {movie.Name} ({movie.Duration} mins)");
            }
        }

        public void AddMovie()
        {
            Console.WriteLine("Enter movie title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter movie duration (in minutes):");
            int duration=int.Parse(Console.ReadLine());

            

            movieService.AddMovie(title, duration);
        }
    }
}
