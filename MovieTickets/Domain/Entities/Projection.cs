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
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }


        public Projection(Movie movie, Hall hall, decimal price, DateTime date)
        {
            Movie = movie;
            Hall = hall;
            Price = price;
            Date = date;
        }

    }
}
