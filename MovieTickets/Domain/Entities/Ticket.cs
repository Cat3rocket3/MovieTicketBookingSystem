using MovieTickets.Domain.Entities;
using System;

public class Ticket
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public bool IsReserved { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ProjectionId { get; set; }
    public virtual Projection Projection { get; set; }

    public int SeatId { get; set; }
    public virtual Seat Seat { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }
}