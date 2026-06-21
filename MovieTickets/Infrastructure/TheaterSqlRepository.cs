using Microsoft.EntityFrameworkCore;
using MovieTickets.Application.interfaces;
using MovieTickets.Data;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Infrastructure
{
    internal class TheaterSqlRepository : ITheaterRepository
    {
        private readonly AppDbContext db;

        public TheaterSqlRepository(AppDbContext db)
        {
            this.db = db;
        }

       

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return db.Movies.Include(m => m.Genre).ToList();
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            db.Movies.Update(movie);
            db.SaveChanges();
        }

        public void RemoveMovie(int id)
        {
            Movie movie = GetMovieById(id);

            if (movie == null)
                throw new Exception("Movie not found.");

            db.Movies.Remove(movie);
            db.SaveChanges();
        }

     

        public IReadOnlyList<Hall> GetAllHalls()
        {
            return db.Halls
                .Include(h => h.Seats)
                .ToList();
        }

        public Hall GetHallById(int id)
        {
            return db.Halls
                .Include(h => h.Seats)
                .FirstOrDefault(h => h.Id == id);
        }

        public void AddHall(Hall hall)
        {
            db.Halls.Add(hall);
            db.SaveChanges();
        }

        public void RemoveHall(int id)
        {
            Hall hall = GetHallById(id);

            if (hall == null)
                throw new Exception("Hall not found.");

            db.Halls.Remove(hall);
            db.SaveChanges();
        }

      

        public Seat GetSeatById(int id)
        {
            return db.Seats.Include(s => s.Hall).FirstOrDefault(s => s.Id == id);
        }

        public void AddSeat(Seat seat)
        {
            db.Seats.Add(seat);
            db.SaveChanges();
        }

        public void RemoveSeat(int id)
        {
            Seat seat = GetSeatById(id);

            if (seat == null)
                throw new Exception("Seat not found.");

            // Prevent removing a seat that has tickets referring to it
            bool hasTickets = db.Tickets.Any(t => t.SeatId == id);
            if (hasTickets)
                throw new Exception("Cannot remove seat because one or more tickets reference it.");

            db.Seats.Remove(seat);
            db.SaveChanges();
        }

       

        public IReadOnlyList<Projection> GetAllProjections()
        {
            return db.Projections
                .Include(p => p.Movie)
                .Include(p => p.Hall)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Seat)
                .ToList();
        }

        public Projection GetProjectionById(int id)
        {
            return db.Projections
                .Include(p => p.Movie)
                .Include(p => p.Hall)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Seat)
                .FirstOrDefault(p => p.Id == id);
        }

        public void AddProjection(Projection projection)
        {
            db.Projections.Add(projection);
            db.SaveChanges();
        }

        public void UpdateProjection(Projection projection)
        {
            db.Projections.Update(projection);
            db.SaveChanges();
        }

        public void RemoveProjection(int id)
        {
            Projection projection = GetProjectionById(id);

            if (projection == null)
                throw new Exception("Projection not found.");

            if (projection.Tickets != null)
            {
                db.Tickets.RemoveRange(projection.Tickets);
            }

            db.Projections.Remove(projection);
            db.SaveChanges();
        }

        

        public List<Ticket> GetAllTickets()
        {
            return db.Tickets
                .Include(t => t.Projection)
                    .ThenInclude(p => p.Movie)
                .Include(t => t.Projection)
                    .ThenInclude(p => p.Hall)
                .Include(t => t.Seat)
                .Include(t => t.User)
                .ToList();
        }

        public Ticket GetTicketById(int id)
        {
            return db.Tickets
                .Include(t => t.Projection)
                    .ThenInclude(p => p.Movie)
                .Include(t => t.Projection)
                    .ThenInclude(p => p.Hall)
                .Include(t => t.Seat)
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == id);
        }

        public void AddTicket(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }

        public void RemoveTicket(int id)
        {
            Ticket ticket = GetTicketById(id);

            if (ticket == null)
                throw new Exception("Ticket not found.");

            db.Tickets.Remove(ticket);
            db.SaveChanges();
        }

        public void UpdateTicket(Ticket ticket)
        {
            db.Tickets.Update(ticket);
            db.SaveChanges();
        }

       
        public IReadOnlyList<Genre> GetAllGenres()
        {
            return db.Genres.ToList();
        }

        public Genre GetGenreById(int id)
        {
            return db.Genres.FirstOrDefault(g => g.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
        }

        public void RemoveGenre(int id)
        {
            Genre genre = GetGenreById(id);

            if (genre == null)
                throw new Exception("Genre not found.");

            db.Genres.Remove(genre);
            db.SaveChanges();
        }

       

        public IReadOnlyList<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void RemoveUser(int id)
        {
            User user = GetUserById(id);

            if (user == null)
                throw new Exception("User not found.");

            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}