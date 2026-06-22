using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class ReportService
    {
        private readonly ITheaterRepository repository;

        public ReportService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public string GenerateTicketText(int ticketId)
        {
            Ticket ticket = repository.GetTicketById(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found.");

            string movieName = ticket.Projection == null || ticket.Projection.Movie == null
                ? "N/A"
                : ticket.Projection.Movie.Name;

            string hall = ticket.Projection == null
                ? "N/A"
                : ticket.Projection.HallId.ToString();

            string date = ticket.Projection == null
                ? "N/A"
                : ticket.Projection.Date.ToString("yyyy-MM-dd HH:mm");

            string customer = ticket.User == null
                ? "Not reserved / paid"
                : ticket.User.FullName;

            return
                "========== MOVIE TICKET ==========\n" +
                $"Customer: {customer}\n" +
                $"Ticket ID: {ticket.Id}\n" +
                $"Movie: {movieName}\n" +
                $"Hall: {hall}\n" +
                $"Row: {ticket.Seat.Row}, Seat: {ticket.Seat.Column}\n" +
                $"Date: {date}\n" +
                $"Price: {ticket.Price} lv\n" +
                $"Status: {GetTicketStatus(ticket)}\n" +
                "==================================";
        }

        public string GetTicketStatus(Ticket ticket)
        {
            if (ticket.IsCancelled)
                return "CANCELLED";

            if (ticket.IsPaid)
                return "PAID";

            if (ticket.IsReserved)
                return "RESERVED";

            return "FREE";
        }

        public string GenerateTicketText(int projectionId, int seatNumber)
        {
            Projection projection = repository.GetProjectionById(projectionId);

            if (projection == null)
                throw new Exception("Projection not found.");

            Ticket ticket = projection.Tickets
                .FirstOrDefault(t => t.Seat != null && t.Seat.Number == seatNumber);

            if (ticket == null)
                throw new Exception("Seat not found.");

            return GenerateTicketText(ticket.Id);
        }
    }
}