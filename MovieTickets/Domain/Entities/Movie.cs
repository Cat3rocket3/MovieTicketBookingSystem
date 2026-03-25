using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }



        public Movie(int id, string name, int duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
            
        }

    }
}
