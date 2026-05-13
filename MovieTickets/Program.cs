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
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=K207\\SQLEXPRESS;Database=MovieTickets;Integrated Security=True;").EnableSensitiveDataLogging().Options;

            // Declare the storage variable
            var db = new AppDbContext(options);

            // Initialize the database connection
            db.Movies

            // Sync the database schema with your C# classes
            db.Database.Migrate();
            db.Update(db.Database);

            // Build the layers: Context -> Repository -> Service -> UI
            ITheaterRepository repository = new TheaterRepository(storage);
            var movieService = new MovieService(repository);
            var ui = new UI(movieService);

            // Start the application
            ui.ShowMainMenu();
        }
    }
}
