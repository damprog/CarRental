using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace CarRental.API.Controllers
{
    // [api/account] - main url for account
    //[Route("api/[controller]")] - unnecessary because route is defined in base class
    public class AccountController : ApiControllerBase
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

        // tu zrobic
        // tu zrobic
        // tu zrobic

        // api/account/{userId} - logged in user
        //[HttpGet("{userId}")] - old, for jwt not required
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAccount() // (Guid userId)
        {
            return Json(await _userService.GetAccountAsync(UserId)); // (userId)
        }

        // tu poprawić
        // tu poprawić
        // tu poprawić aczkolwiek nie trzeba póki co

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

        // tu zrobic
        // tu zrobic
        // tu zrobic

        // api/account/rentals/{userId} - all rent orders for logged in user
        [Authorize]
        [HttpGet("rentals")] 
        public async Task<IActionResult> GetRentals()
        {
            // get userId from jwt claims
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            
            if(!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string jwtToken = token.Substring("Bearer ".Length);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(jwtToken);

                var account = await _userService.GetAccountAsync(Guid.Parse(jwt.Subject));
                var rentals = await _rentalService.GetForUserAsync(account.Id);
                if (rentals == null)
                {
                    return NotFound();
                }
                return Json(rentals);
            }
            else
            {
                return Unauthorized();
            }
        }

        // api/account/rentals - post method should add rental
        [Authorize]
        [HttpPost("rentals")] 
        public async Task<IActionResult> PostRental([FromBody]CreateRentalDto rental)
        {
            // get userId from jwt claims
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                string jwtToken = token.Substring("Bearer ".Length);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(jwtToken);

                var user = await _userService.GetAccountAsync(Guid.Parse(jwt.Subject));
                var car = await _carService.GetAsync(rental.CarId);
                var id = Guid.NewGuid();
                await _rentalService.CreateAsync(id, car, user, rental.StartDate, rental.EndDate);
                return Created("api/account/rentals", null);
            }
            else
            {
                return Unauthorized();
            }
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
