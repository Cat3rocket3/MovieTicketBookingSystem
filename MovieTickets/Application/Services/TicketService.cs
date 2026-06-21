using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class TicketService
    {
        private readonly ITheaterRepository repository;

        public TicketService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<Ticket> GetAllTickets()
        {
            return repository.GetAllTickets();
        }

        public Ticket GetTicketById(int id)
        {
            return repository.GetTicketById(id);
        }

        public void ReserveTicket(int projectionId, int seatNumber, int? userId = null)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat != null && t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsPaid)
                throw new Exception("Seat is already paid.");

            if (ticket.IsReserved && !ticket.IsCancelled)
                throw new Exception("Seat is already reserved.");

            ticket.IsReserved = true;
            ticket.IsPaid = false;
            ticket.IsCancelled = false;
            ticket.UserId = userId;

            repository.UpdateTicket(ticket);
        }

        public void PayTicket(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsCancelled)
                throw new Exception("Ticket is cancelled.");

            if (!ticket.IsReserved)
                throw new Exception("Ticket must be reserved first.");

            if (ticket.IsPaid)
                throw new Exception("Ticket already paid.");

            ticket.IsPaid = true;

            repository.UpdateTicket(ticket);
        }

        public void CancelReservation(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            if (ticket.IsPaid)
                throw new Exception("Paid ticket cannot be cancelled.");

            if (!ticket.IsReserved)
                throw new Exception("Ticket is not reserved.");

            ticket.IsReserved = false;
            ticket.IsPaid = false;
            ticket.IsCancelled = false; 
            ticket.UserId = null;

            repository.UpdateTicket(ticket);
        }

        

        public IReadOnlyList<Ticket> GetBookingHistory(int userId)
        {
            return repository.GetAllTickets()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }
    }
}