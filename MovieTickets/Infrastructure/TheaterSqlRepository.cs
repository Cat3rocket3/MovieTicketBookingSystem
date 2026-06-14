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

        // ================= MOVIES =================

        public IReadOnlyList<Movie> GetAllMovies()
        {
            return db.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.FirstOrDefault(m => m.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
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

        // ================= HALLS =================

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

        // ================= PROJECTIONS =================

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

        // ================= TICKETS =================

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
    }
}