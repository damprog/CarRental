using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;

        public AccountController(IUserService userService, IRentalService rentalService, ICarService carService)
        {
            _userService = userService;
            _rentalService = rentalService;
            _carService = carService;
        }

        // api/account/{userId} - logged in user
        [HttpGet("{userId}")] 
        public async Task<IActionResult> GetAccount(Guid userId)
        {
            return Json(await _userService.GetAccountAsync(userId));
        }

        // api/account/register - here data should be passed for register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Register register)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), register.Email, register.Name, register.Password);
            return Created("/account/{userId}", null);
        }

        // api/account/login - here data should be passed for login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            var user = await _userService.LoginAsync(login.Email, login.Password);
            return Json(user);
        }

        // api/account/rentals/{userId} - all rent orders for logged in user
        [HttpGet("rentals/{userId}")]
        public async Task<IActionResult> GetRentals(Guid userId)
        {
            var account = await _userService.GetAccountAsync(userId);
            var rentals = await _rentalService.GetForUserAsync(account.Id);
            if (rentals == null)
            {
                return NotFound();
            }
            return Json(rentals);
        }

        // api/account/rentals - post method should add rental
        [HttpPost("rentals")]
        public async Task<IActionResult> PostRental([FromBody]CreateRentalDto rental)
        {
            var user = await _userService.GetAccountAsync(rental.UserId);
            var car = await _carService.GetAsync(rental.CarId);
            var id = Guid.NewGuid();
            await _rentalService.CreateAsync(id, car, user, rental.StartDate, rental.EndDate);
            return Created("api/account/rentals", null);
        }

        [HttpPut("rentals/id/{rentalId}")]
        public async Task<IActionResult> UpdateRental(Guid rentalId, [FromBody]UpdateRentalDto rental)
        {
            var user = await _userService.GetAccountAsync(rental.UserId);
            var car = await _carService.GetAsync(rental.CarId);
            await _rentalService.UpdateAsync(rentalId, car, user, rental.StartDate, rental.EndDate);
            return NoContent();
        }

        [HttpDelete("rentals/id/{rentalId}")]
        public async Task<IActionResult> DeleteRental(Guid rentalId)
        {
            await _rentalService.DeleteAsync(rentalId);
            return NoContent();
        }
    }
}
