using CarRental.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetAsync(Guid id);
        Task<IEnumerable<Rental>> GetForUserAsync(Guid id);
        Task<IEnumerable<Rental>> GetForCarAsync(Guid id);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Rental rental);
    }
}
