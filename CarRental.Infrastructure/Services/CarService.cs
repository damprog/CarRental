using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.DTO.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) 
        { 
            _carRepository = carRepository;
        }

        public async Task<CarDto> GetAsync(Guid id)
        {
            var car = await _carRepository.GetAsync(id);

            if(car == null)
            {
                return null;
            }

            return new CarDto()
            {
                Id = car.Id,
                Name = car.Name,
                Description = car.Description,
                PricePerDay = car.PricePerDay,
                PricePerHour = car.PricePerHour
            };
        }

        public async Task<IEnumerable<CarDto>> BrowseAsync(string name = null)
        {
            var cars = await _carRepository.BrowseAsync(name);

            if (cars == null)
            {
                return null;
            }

            return cars.Select(car => new CarDto()
            {
                Id = car.Id,
                Name = car.Name,
                Description = car.Description,
                PricePerDay = car.PricePerDay,
                PricePerHour = car.PricePerHour,
            });
        }

        public async Task CreateAsync(Guid id, string name, string description, decimal PricePerDay, decimal PricePerHour)
        {
            var car = new Car(id, name, description, PricePerDay, PricePerHour);
            await _carRepository.AddAsync(car);
        }

        public async Task UpdateAsync(Guid id, string name, string description, decimal PricePerDay, decimal PricePerHour)
        {
            var car = await _carRepository.GetAsync(id);
            if(car == null)
            {
                throw new Exception($"Car with id: '{id}' does not exist.");
            }

            car.SetName(name);
            car.SetDescription(description);
            car.SetPricePerDay(PricePerDay);
            car.SetPricePerHour(PricePerHour);
            await _carRepository.UpdateAsync(car);
        }

        public async Task DeleteAsync(Guid id)
        {
            var car = await _carRepository.GetAsync(id);
            if (car == null)
            {
                throw new Exception($"Car with id: '{id}' does not exist.");
            }

            await _carRepository.DeleteAsync(car);
        }
    }
}
