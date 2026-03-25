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
        public Projection Projection { get; set; }
        public Seat Seat { get; set; }
        public decimal Price { get; set; }
        public Ticket(int id, Projection projection, Seat seat)
        {
            Id = id;
            Projection = projection;
            Seat = seat;
            Price = Projection.Price;
        }
    }
}
