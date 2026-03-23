using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Projection
    {
       public Movie Movie { get; set; }
        Hall Hall { get; set; }
        public decimal Price { get; set; }
        DateTime Date { get; set; }


        public Projection(Movie movie, Hall hall, decimal price, DateTime date)
        {
            Movie = movie;
            Hall = hall;
            Price = price;
            Date = date;
        }

    }
}
