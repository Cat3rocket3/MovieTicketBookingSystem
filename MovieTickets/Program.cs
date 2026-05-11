using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieTickets.Application;
using MovieTickets.Application.interfaces;
using MovieTickets.Application.Services;
using MovieTickets.Data;
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

            // Declare the storage variable
            AppDbContext storage;

            // Initialize the database connection
            using (var db = new AppDbContext())
            {
                // Sync the database schema with your C# classes
                db.Database.Migrate();

                // Assign the context to your storage design
                storage = db;

                // Build the layers: Context -> Repository -> Service -> UI
                ITheaterRepository repository = new TheaterRepository(storage);
                var movieService = new MovieService(repository);
                var ui = new UI(movieService);

                // Start the application
                ui.ShowMainMenu();
            }
        }
    }
}
