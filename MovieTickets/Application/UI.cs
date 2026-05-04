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
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Remove Movie");
                Console.WriteLine("4. View Halls");
                Console.WriteLine("5. Add Hall");
                Console.WriteLine("6. Remove Hall");
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
                        RemoveMovie();
                        break;
                    case "4":
                        ShowHalls();
                        break;
                    //case "5":
                    //    AddHall();
                    //    break;
                    //case "6":
                    //    ShowHalls();
                    //    RemoveHall();
                    //    break;
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

        private void RemoveHall()
        {
            if (movieService.GetAllHalls().Count == 0)
            {
                return;
            }
            Console.Write("Enter Hall ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            movieService.RemoveHall(id);
        }

        private void AddHall()
        {
            throw new NotImplementedException(); // ne pipai dokato ne se opravqt problemite s seat id
            Console.WriteLine("Enter number of rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of columns: ");
            int columns = int.Parse(Console.ReadLine());
            movieService.AddHall(rows, columns);
        }

        private void ShowHalls()
        {
            var halls = movieService.GetAllHalls();
            Console.WriteLine("Halls:");
            if (halls.Count == 0)
            {
                Console.WriteLine("(No halls at this moment)");
                return;
            }
            foreach (var hall in halls)
            {
                Console.WriteLine($"{hall.Id}. (Capacity: {hall.Seats.Count})");
            }
        }

        public void ShowMovies()
        {
            var movies = movieService.GetAllMovies();
            Console.WriteLine("Movies:");
            if (movies.Count == 0)
            {
                Console.WriteLine("(No movies at this moment)");
                return;
            }
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Id}. {movie.Name} ({movie.Duration} mins)");
            }
        }

        public void AddMovie()
        {
            Console.Write("Enter movie title: ");
            string title = Console.ReadLine();
            Console.Write("Enter movie duration (in minutes): ");
            int duration = int.Parse(Console.ReadLine());
            movieService.AddMovie(title, duration);
        }

        public void RemoveMovie()
        {
            if(movieService.GetAllMovies().Count == 0)
            {
                return;
            }
            Console.Write("Enter movie ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            movieService.RemoveMovie(id);
        }
    }
}
