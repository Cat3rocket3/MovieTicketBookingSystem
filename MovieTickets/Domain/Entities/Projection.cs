using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Projection
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        // Foreign Keys
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public int HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Projection() { }

    }
}
