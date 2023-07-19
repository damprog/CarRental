using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.DTO.Cars;
using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalrepository;

        public RentalService(IRentalRepository rentalrepository)
        {
            _rentalrepository = rentalrepository;
        }

        public async Task<RentalDto> GetAsync(Guid id)
        {
            var rental = await _rentalrepository.GetAsync(id);

            if(rental == null)
            {
                return null;
            }

            return new RentalDto()
            {
                Id = rental.Id,
                CarId = rental.CarId,
                CarName = rental.CarName,
                UserId = rental.UserId,
                UserName = rental.UserName,
                RentPrice = rental.RentPrice,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                CreatedAt = rental.CreatedAt
            };
        }

        public async Task<IEnumerable<RentalDto>> GetForCarAsync(Guid carId)
        {
            var rentals = await _rentalrepository.GetForCarAsync(carId);

            if (rentals == null)
            {
                return null;
            }

            return rentals.Select(rental => new RentalDto()
            {
                Id = rental.Id,
                CarId = rental.CarId,
                CarName = rental.CarName,
                UserId = rental.UserId,
                UserName = rental.UserName,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                CreatedAt = rental.CreatedAt,
                RentPrice = rental.RentPrice
            });
        }

        public async Task<IEnumerable<RentalDto>> GetForUserAsync(Guid userId)
        {
            var rentals = await _rentalrepository.GetForUserAsync(userId);

            if (rentals == null)
            {
                return null;
            }

            return rentals.Select(rental => new RentalDto()
            {
                Id = rental.Id,
                CarId = rental.CarId,
                CarName = rental.CarName,
                UserId = rental.UserId,
                UserName = rental.UserName,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                CreatedAt = rental.CreatedAt,
                RentPrice = rental.RentPrice
            });
        }

        public async Task CreateAsync(Guid id, CarDto car, AccountDto user, DateTime start, DateTime end)
        {
            var rental = new Rental(id, car.Id, car.Name, car.PricePerDay, car.PricePerHour, user.Id, user.Name, start, end);
            await _rentalrepository.AddAsync(rental);
        }
        
        public async Task UpdateAsync(Guid id, CarDto car, AccountDto user, DateTime start, DateTime end)
        {
            var rental = await _rentalrepository.GetAsync(id);
            if(rental == null)
            {
                throw new Exception($"Rental with id: '{id}' does not exist.");
            }

            rental.SetCar(car.Id, car.Name);
            rental.SetUser(user.Id, user.Name);
            rental.SetDates(start, end);
            rental.SetRentPrice(car.PricePerDay, car.PricePerHour);
            await _rentalrepository.UpdateAsync(rental);
        }

        public async Task DeleteAsync(Guid id)
        {
            var rental = await _rentalrepository.GetAsync(id);
            if(rental == null)
            {
                throw new Exception($"Rental with id: '{id}' does not exist.");
            }

            await _rentalrepository.DeleteAsync(rental);
        }
    }
}
