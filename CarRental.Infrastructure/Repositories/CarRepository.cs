using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<Car> GetAsync(Guid id)  
            => await Task.FromResult(_context.Cars.SingleOrDefault(x => x.Id == id));

        public async Task<IEnumerable<Car>> BrowseAsync(string name = "")
        {
            var cars = _context.Cars.AsEnumerable();
            if(!string.IsNullOrEmpty(name))
            {
                cars = cars.Where(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(cars);
        }

        public async Task AddAsync(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            await Task.FromResult(car);
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
