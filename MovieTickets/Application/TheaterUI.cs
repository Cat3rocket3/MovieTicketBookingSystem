using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieTickets.Infrastructure;

namespace MovieTickets.Application
{
    internal class TheaterUI
    {
        public void ShowMainMenu()
        {
            
            TheaterRepository theaterRepository = new TheaterRepository(new FileStorage("theater.json").Load());

            Console.WriteLine("Welcome to the Movie Theater!");
            Console.WriteLine("1. View Movies");
            Console.WriteLine("2.Add Movie");
            Console.WriteLine("3.Remove Movie");
            Console.WriteLine("3. Exit");




            bool running = true;
            while (running)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                       
                        break;
                    case "2":
                        // Add movie
                        break;
                    case "3":
                        // Remove movie
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        running = false;
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            
        }
    }
}
