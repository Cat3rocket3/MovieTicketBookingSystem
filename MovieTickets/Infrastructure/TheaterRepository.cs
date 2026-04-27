using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTickets.Domain.Entities;
using MovieTickets.Application.interfaces;

namespace MovieTickets.Infrastructure
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly FileStorage storage;

        public TheaterRepository(FileStorage storage)
        {
            this.storage = storage;
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            var db = storage.Load();
            return db.Movies;
        }

        public Movie GetMovieById(int id)
        {
            var db = storage.Load();
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
        {var db = storage.Load();
            movie.Id = db.NextMovieId;
            db.NextMovieId++;
            db.Movies.Add(movie);
            storage.Save(db);
        }

        public IReadOnlyList<Hall> GetAllHalls()
        {
            var db = storage.Load();
            return db.Halls;
        }

        public Hall GetHallById(int id)
        {
            var db = storage.Load();
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
            var db = storage.Load();
            hall.Id = db.NextHallId;
            db.NextHallId++;
            db.Halls.Add(hall);
            storage.Save(db);
        }

        public IReadOnlyList<Projection> GetAllProjections()
        {
            var db = storage.Load();
            return db.Projections;
        }

        public Projection GetProjectionById(int id)
        {
            var db = storage.Load();
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
            var db = storage.Load();
            projection.Id = db.NextProjectionId;
            db.NextProjectionId++;
            db.Projections.Add(projection);
            storage.Save(db);
        }

        public IReadOnlyList<Ticket> GetAllTickets()
        {
            var db = storage.Load();
            return db.Tickets;
        }

        public Ticket GetTicketById(int id)
        {
            var db = storage.Load();
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
            var db = storage.Load();
            ticket.Id = db.NextTicketId;
            db.NextTicketId++;
            db.Tickets.Add(ticket);
            storage.Save(db);
        }
    }
}