using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalContext _context;

        public UserRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid id) 
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
        {
            var users = await _context.Users.ToListAsync();
            return users.SingleOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
        

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
