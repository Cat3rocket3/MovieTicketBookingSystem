using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Hall
    {
        public int Id { get; set; }
        public List<Seat> Seats { get; set; }

        public Hall(int id, List<Seat> seats)
        {
            Id = id;
            Seats = seats;
        }
    }
}
