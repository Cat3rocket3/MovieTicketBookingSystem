using MovieTickets.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public bool IsSold { get; set; }

    public int ProjectionId { get; set; }
    public virtual Projection Projection { get; set; }

    public int SeatId { get; set; }
    public virtual Seat Seat { get; set; }
}