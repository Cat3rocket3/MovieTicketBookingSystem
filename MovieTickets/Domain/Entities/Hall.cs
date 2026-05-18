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
        // Use ICollection for EF compatibility, but it accepts List
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public virtual ICollection<Projection> Projections { get; set; }

        // 1. Parameterless constructor for EF Core
        public Hall() { }

        // 2. The constructor your MovieService is looking for
        public Hall( List<Seat> seats)
        {
            Seats = seats;
        }
    }
}
