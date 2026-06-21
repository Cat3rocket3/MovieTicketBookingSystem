using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class HallService
    {
        private readonly ITheaterRepository repository;

        public HallService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<Hall> GetAllHalls()
        {
            return repository.GetAllHalls();
        }

        public Hall GetHallById(int id)
        {
            return repository.GetHallById(id);
        }

        public void AddHall(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new Exception("Rows and columns must be positive.");

            List<Seat> seats = new List<Seat>();

            int seatNumber = 1;

            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= columns; column++)
                {
                    Seat seat = new Seat(row, column, seatNumber);
                    seats.Add(seat);

                    seatNumber++;
                }
            }

            Hall hall = new Hall(seats);
            repository.AddHall(hall);
        }

        public void RemoveHall(int id)
        {
            repository.RemoveHall(id);
        }

       

        public void AddSeat(int hallId, int row, int column)
        {
            Hall hall = repository.GetHallById(hallId);

            if (hall == null)
                throw new Exception("Hall not found.");

            bool exists = hall.Seats.Any(s => s.Row == row && s.Column == column);

            if (exists)
                throw new Exception("A seat already exists at this row/column.");

            int nextNumber = hall.Seats.Count == 0 ? 1 : hall.Seats.Max(s => s.Number) + 1;

            Seat seat = new Seat(row, column, nextNumber);
            seat.HallId = hallId;

            repository.AddSeat(seat);
        }

        public void RemoveSeat(int seatId)
        {
            repository.RemoveSeat(seatId);
        }
    }
}