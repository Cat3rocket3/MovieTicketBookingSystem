using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool IsAvailable { get; set; }
        public Hall Hall { get; set; }

        public Seat(int id, int row, int column)
        {
            Id = id;
            Row = row;
            Column = column;
            IsAvailable = true;
        }
    }
}
