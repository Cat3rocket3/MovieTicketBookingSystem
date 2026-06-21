using MovieTickets.Application.interfaces;
using MovieTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.Application.Services
{
    public class UserService
    {
        private readonly ITheaterRepository repository;

        public UserService(ITheaterRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<User> GetAllUsers()
        {
            return repository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return repository.GetUserById(id);
        }

        public void AddUser(string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Exception("User name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Exception("A valid email is required.");

            bool emailTaken = repository.GetAllUsers()
                .Any(u => u.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (emailTaken)
                throw new Exception("A user with this email already exists.");

            User user = new User { FullName = fullName, Email = email };
            repository.AddUser(user);
        }

        public void RemoveUser(int id)
        {
            repository.RemoveUser(id);
        }
    }
}