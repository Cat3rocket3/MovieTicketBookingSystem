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

           
            var db = new AppDbContext(options);

            
          

            
            db.Database.Migrate();
            db.Update(db.Database);

            ITheaterRepository repository = new TheaterSqlRepository(db);
            var movieService = new MovieService(repository);
            var ui = new UI(movieService);

           

            ui.ShowMainMenu();
        }
    }
}
