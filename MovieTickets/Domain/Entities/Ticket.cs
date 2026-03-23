using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    internal class Ticket
    {
        int Id { get; set; }
        Projection Projection { get; set; }
        Seat Seat { get; set; }
        decimal Price { get; set; }
        public Ticket(int id, Projection projection, Seat seat)
        {
            Id = id;
            Projection = projection;
            Seat = seat;
            Price = Projection.Price;
        }
    }
}
