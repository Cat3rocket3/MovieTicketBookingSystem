namespace MovieTickets.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public int HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public Seat()
        {
        }

        public Seat(int row, int column, int number)
        {
            Row = row;
            Column = column;
            Number = number;
        }
    }
}