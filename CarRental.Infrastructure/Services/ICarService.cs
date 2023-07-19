using CarRental.Core.Domain;
using CarRental.Infrastructure.DTO.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public interface ICarService
    {
        Task<CarDto> GetAsync(Guid id);
        Task<IEnumerable<CarDto>> BrowseAsync(string name = null);
        Task CreateAsync(Guid id, string name, string description, decimal PricePerDay, decimal PricePerHour);
        Task UpdateAsync(Guid id, string name, string description, decimal PricePerDay, decimal PricePerHour);
        Task DeleteAsync(Guid id);
    }
}
