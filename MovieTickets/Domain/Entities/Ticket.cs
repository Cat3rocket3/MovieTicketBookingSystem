using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int ProjectionId { get; set; }
        public virtual Projection Projection { get; set; }

        public int SeatId { get; set; }
        public virtual Seat Seat { get; set; }

        public Ticket() { }
        public Ticket(int id, Projection projection, Seat seat)
        {
            Id = id;
            Projection = projection;
            Seat = seat;
            Price = projection?.Price ?? 0;
        }
    }
}
