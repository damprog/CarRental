using CarRental.Core.Domain;
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
    public interface IRentalService
    {
        Task<RentalDto> GetAsync(Guid id);
        Task<IEnumerable<RentalDto>> GetForCarAsync(Guid carId);
        Task<IEnumerable<RentalDto>> GetForUserAsync(Guid userId);
        Task CreateAsync(Guid id, CarDto car, AccountDto user, DateTime start, DateTime end);
        Task UpdateAsync(Guid id, CarDto car, AccountDto user, DateTime start, DateTime end);
        Task DeleteAsync(Guid id);
    }
}
