using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;

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
    }
}