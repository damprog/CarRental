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
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalContext _context;

        public RentalRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<Rental> GetAsync(Guid id)
            => await Task.FromResult(_context.Rentals.SingleOrDefault(x => x.Id == id));

        public async Task<IEnumerable<Rental>> GetForCarAsync(Guid carId)
        {
            var rentals = _context.Rentals.AsEnumerable();
            rentals = rentals.Where(x => x.CarId == carId);
            return await Task.FromResult(rentals);
        }

        public async Task<IEnumerable<Rental>> GetForUserAsync(Guid userId)
        {
            var rentals = _context.Rentals.AsEnumerable();
            rentals = rentals.Where(x => x.UserId == userId);
            return await Task.FromResult(rentals);
        }

        public async Task AddAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Rental rental)
        {
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
