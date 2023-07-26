using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.DTO;
using CarRental.Infrastructure.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task<AccountDto> GetAccountAsync(Guid id)
        {
            var account = await _userRepository.GetAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with id: '{id}' does not exist.");
            }
            return new AccountDto()
            {
                Id = account.Id,
                Role = account.Role,
                Name = account.Name,
                Email = account.Email
            };
        }

        // This is old version of loging users
        /* public async Task<AccountDto> LoginAsync(string email, string password)
         {
             var user = await _userRepository.GetAsync(email);
             if (user == null)
             {
                 throw new Exception($"Invalid credentials.");
             }
             if(user.Password != password)
             {
                 throw new Exception($"Invalid credentials.");
             }
             return new AccountDto()
             {
                 Id = user.Id,
                 Role = user.Role,
                 Name = user.Name,
                 Email = user.Email
             };
         }*/

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }
            if (user.Password != password)
            {
                throw new Exception($"Invalid credentials.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role, user.Name);

            return new TokenDto
            {
                JWT = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role,
                Username = user.Name
            };
        }

        public async Task RegisterAsync(Guid id, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null) 
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            user = new User(id, role, name, email, password);
            await _userRepository.AddAsync(user);
        }
    }
}
