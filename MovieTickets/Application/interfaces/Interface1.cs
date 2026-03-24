using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Application.interfaces
{
    public interface ITheaterRepository
    {
        IReadOnlyList<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);

        IReadOnlyList<Hall> GetAllHalls();
        Hall GetHallById(int id);
        void AddHall(Hall hall);

        IReadOnlyList<Projection> GetAllProjections();
        Projection GetProjectionById(int id);
        void AddProjection(Projection projection);

        IReadOnlyList<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);
        void AddTicket(Ticket ticket);
    }
}
