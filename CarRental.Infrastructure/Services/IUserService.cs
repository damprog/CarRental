using CarRental.Core.Domain;
using CarRental.Infrastructure.DTO;
using CarRental.Infrastructure.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid id);

        Task RegisterAsync(Guid id, string email, string name,  string password, string role = "user");

        //Task<AccountDto> LoginAsync(string email, string password);
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
