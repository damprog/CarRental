using CarRental.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetAsync(Guid id);
        Task<IEnumerable<Car>> BrowseAsync(string name = "");
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
    }
}
