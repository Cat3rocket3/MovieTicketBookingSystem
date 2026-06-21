using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTickets.Domain.Entities;

namespace MovieTickets.Application.interfaces
{
    public interface ITheaterRepository
    {
        IReadOnlyList<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void RemoveMovie(int id);

        IReadOnlyList<Hall> GetAllHalls();
        Hall GetHallById(int id);
        void AddHall(Hall hall);
        void RemoveHall(int id);

        Seat GetSeatById(int id);
        void AddSeat(Seat seat);
        void RemoveSeat(int id);

        IReadOnlyList<Projection> GetAllProjections();
        Projection GetProjectionById(int id);
        void AddProjection(Projection projection);
        void UpdateProjection(Projection projection);
        void RemoveProjection(int id);

        List<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);

        void AddTicket(Ticket ticket);
        void RemoveTicket(int id);
        void UpdateTicket(Ticket ticket);

        IReadOnlyList<Genre> GetAllGenres();
        Genre GetGenreById(int id);
        void AddGenre(Genre genre);
        void RemoveGenre(int id);

        IReadOnlyList<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void RemoveUser(int id);
    }
}