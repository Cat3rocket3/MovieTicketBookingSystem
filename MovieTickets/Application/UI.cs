using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTickets.Application.Services; 
using MovieTickets.Application.interfaces;
using MovieTickets.Infrastructure;
using System.Diagnostics;

namespace MovieTickets.Application
{
    internal class UI
    {
         private readonly MovieService service;

        public UI(MovieService movieService)
        {
            this.service = movieService;
        }


        public void ShowMainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to the Movie Theater!");
                Console.WriteLine("1. Show Movies");
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Remove Movie");
                Console.WriteLine("4. Edit Movie");
                Console.WriteLine("5. Show Projections");
                Console.WriteLine("6. Add Projection");
                Console.WriteLine("7. Remove Projection");
                Console.WriteLine("8. Edit Projection");
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    Console.WriteLine();

                    case "1":
                        ShowMovies();
                        break;
                    case "2":
                        AddMovie();
                        break;
                    case "3":
                        RemoveMovie();
                        break;
                    case "4":
                        EditMovie();
                        break;
                    case "5":
                        ShowProjections();
                        break;
                    case "6":
                        AddProjection();
                        break;
                    case "7":
                        RemoveProjection();
                        break;
                    case "8":
                        EditProjection();
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
         var movies = service.GetAllMovies();
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

            service.AddMovie(title, duration);
        }

        public void RemoveMovie()
        {
            Console.WriteLine("Enter movie ID to remove:");
            int id = int.Parse(Console.ReadLine());
            service.RemoveMovie(id);
        }

        public void EditMovie()
        {
            Console.WriteLine("Enter movie ID to edit:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new movie title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter new movie duration (in minutes):");
            int duration = int.Parse(Console.ReadLine());

            service.EditMovie(id, title, duration);
        }

        public void AddProjection()
        {
            Console.Write("Enter movie ID for projection: ");
            int movieId = int.Parse(Console.ReadLine());

            Console.Write("Enter hall ID for projection: ");
            int hallId = int.Parse(Console.ReadLine());

            Console.Write("Enter ticket price for projection: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter projection time (e.g., 2024-12-31 19:00): ");
            DateTime time = DateTime.Parse(Console.ReadLine());

            service.AddProjection(movieId, time);
        }

        public void RemoveProjection()
        {
            
            Console.Write("Enter projection ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            service.RemoveProjection(id);
        }

        public void EditProjection()
        {
            Console.Write("Enter projection ID to edit: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new movie ID for projection: ");
            int movieId = int.Parse(Console.ReadLine());

            Console.Write("Enter new projection time (e.g., 2024-12-31 19:00): ");
            DateTime time = DateTime.Parse(Console.ReadLine());

            service.EditProjection(id, movieId, time);
        }

        public void ShowProjections()
        {
            var projections = service.GetAllProjections();
            Console.WriteLine("Projections:");
            foreach (var projection in projections)
            {
                Console.WriteLine($"{projection.Id}. Movie ID: {projection.MovieId}, Time: {projection.Time}");
            }
        }
    }
