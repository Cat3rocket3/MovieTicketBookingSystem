using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Infrastructure
{
   public class TheaterStorage
    {
        public int NextMovieId { get; set; } = 1;
        public int NextHallId { get; set; } = 1;
        public int NextSeatId { get; set; } = 1;
        public int NextProjectionId { get; set; } = 1;
     
        public int NextTicketId { get; set; } = 1;

        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Hall> Halls { get; set; } = new List<Hall>();
        public List<Projection> Projections { get; set; } = new List<Projection>();
     
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
