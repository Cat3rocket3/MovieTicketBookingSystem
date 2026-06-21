using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class ProjectionService
    {
        private readonly ITheaterRepository repository;

        public ProjectionService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<Projection> GetAllProjections()
        {
            return repository.GetAllProjections();
        }

        public void AddProjection(int movieId, int hallId, decimal price, DateTime date)
        {
            Movie movie = repository.GetMovieById(movieId);
            Hall hall = repository.GetHallById(hallId);

            if (movie == null)
                throw new Exception("Movie not found.");

            if (hall == null)
                throw new Exception("Hall not found.");

            if (price <= 0)
                throw new Exception("Price must be positive.");

            if (HasScheduleConflict(hallId, date, movie.Duration))
                throw new Exception("This hall already has a projection during this time.");

            Projection projection = new Projection
            {
                MovieId = movieId,
                HallId = hallId,
                Price = price,
                Date = date,
                Movie = movie,
                Hall = hall,
                Tickets = new List<Ticket>()
            };

            foreach (Seat seat in hall.Seats)
            {
                Ticket ticket = new Ticket
                {
                    Seat = seat,
                    SeatId = seat.Id,
                    Price = price,
                    IsReserved = false,
                    IsPaid = false,
                    IsCancelled = false
                };

                projection.Tickets.Add(ticket);
            }

            repository.AddProjection(projection);
        }

        private bool HasScheduleConflict(int hallId, DateTime newStart, int newMovieDuration)
        {
            DateTime newEnd = newStart.AddMinutes(newMovieDuration);

            var projections = repository.GetAllProjections()
                .Where(p => p.HallId == hallId)
                .ToList();

            foreach (Projection projection in projections)
            {
                if (projection.Movie == null)
                    continue;

                DateTime existingStart = projection.Date;
                DateTime existingEnd = projection.Date.AddMinutes(projection.Movie.Duration);

                bool overlaps = newStart < existingEnd && newEnd > existingStart;

                if (overlaps)
                    return true;
            }

            return false;
        }

        public void RemoveProjection(int id)
        {
            repository.RemoveProjection(id);
        }
    }
}