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
        private readonly TheaterStorage storage;

        public TheaterRepository(TheaterStorage storage)
        {
            this.storage = storage;
        }

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return storage.Movies;
        }

        public Movie GetMovieById(int id)
        {
            foreach (Movie movie in storage.Movies)
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
            if (movie.Id == 0)
            {
                movie.Id = storage.NextMovieId++;
                storage.Movies.Add(movie);
            }
            else
            {
                bool found = false;
                for (int i = 0; i < storage.Movies.Count; i++)
                {
                    if (storage.Movies.Id == movie.Id)
                    {
                        storage.Movies[i] = movie;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    throw new Exception("Movie not found");
                }
            }

        }

        public void RemoveMovie(int id)
        {
            Movie movie = GetMovieById(id);
            
            if (movie != null)
            {
                storage.Movies.Remove(movie);
            }
        }

        public IReadOnlyList<Hall> GetAllHalls()
        {
            return storage.Halls;
        }

        public Hall GetHallById(int id)
        {
            foreach (Hall hall in storage.Halls)
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
            hall.Id = storage.NextHallId;
            storage.NextHallId++;
            storage.Halls.Add(hall);
        }

        public IReadOnlyList<Projection> GetAllProjections()
        {
            return storage.Projections;
        }

        public Projection GetProjectionById(int id)
        {
            foreach (Projection projection in storage.Projections)
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
            projection.Id = storage.NextProjectionId;
            storage.NextProjectionId++;
            storage.Projections.Add(projection);
        }

        public IReadOnlyList<Ticket> GetAllTickets()
        {
            return storage.Tickets;
        }

        public Ticket GetTicketById(int id)
        {
            foreach (Ticket ticket in storage.Tickets)
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
            ticket.Id = storage.NextTicketId;
            storage.NextTicketId++;
            storage.Tickets.Add(ticket);
        }
    }
}