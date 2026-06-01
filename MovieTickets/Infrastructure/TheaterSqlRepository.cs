using Microsoft.EntityFrameworkCore;
using MovieTickets.Data;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Infrastructure
{
    internal class TheaterSqlRepository : MovieTickets.Application.interfaces.ITheaterRepository
    {
        AppDbContext db;

       
        public TheaterSqlRepository(AppDbContext db)
        {
            this.db = db;
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            
            return db.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
          
            foreach (Movie movie in db.Movies)
            {
                if (movie.Id == id)
                {
                    return movie;
                }
            }

            return null;
        }


        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void RemoveMovie(int id)
        {
            var movie = GetMovieById(id);
            if (movie != null)
            {
                db.Movies.Remove(movie);

                db.SaveChanges();
                Console.WriteLine("Movie removed.");
            }

            else Console.WriteLine("Movie with this ID does not exist.");
        }

        public IReadOnlyList<Hall> GetAllHalls()
        {
            return db.Halls.Include(h => h.Seats).ToList();
        }

        public Hall GetHallById(int id)
        {
            foreach (Hall hall in db.Halls)
            {
                if (hall.Id == id)
                {
                    return hall;
                }
            }

            return null;
        }

        public void AddHall(Hall hall)
        {
            db.Halls.Add(hall);
            db.SaveChanges();
        }

        public void RemoveHall(int id)
        {
            var hall = GetHallById(id);
            if (hall != null)
            {

                db.Halls.Remove(hall);
                db.SaveChanges();
            }

            else Console.WriteLine("Hall with this ID does not exist.");
        }



        public IReadOnlyList<Projection> GetAllProjections()
        {
            return db.Projections.ToList();
        }

        public Projection GetProjectionById(int id)
        {
            foreach (Projection projection in db.Projections)
            {
                if (projection.Id == id)
                {
                    return projection;
                }
            }

            return null;
        }

        public void AddProjection(Projection projection)
        {
            db.Projections.Add(projection);
            db.SaveChanges();
        }

        public void RemoveProjection(int id)
        {
            var projection = GetProjectionById(id);
            if (projection != null)
            {

                db.Projections.Remove(projection);
                db.SaveChanges();
            }

            else Console.WriteLine("Hall with this ID does not exist.");
        }

        public IReadOnlyList<Ticket> GetAllTickets()
        {

            return db.Tickets.ToList();
        }

        public Ticket GetTicketById(int id)
        {
            foreach (Ticket ticket in db.Tickets)
            {
                if (ticket.Id == id)
                {
                    return ticket;
                }
            }

            return null;
        }

        public void AddTicket(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }
    }
}
