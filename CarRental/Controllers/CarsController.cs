using CarRental.Infrastructure.DTO.Cars;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Policy = "HasAdminRole")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IRentalService _rentalService;

        public CarsController(ICarService carService, IRentalService rentalService)
        {
            _carService = carService;
            _rentalService = rentalService;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAll(string name)
        {
            //throw new ArgumentException("Ups ... "); // for testing middleware
            var cars = await _carService.BrowseAsync(name);
            return Json(cars);
        }

        [HttpGet("id/{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            return Json(car);
        }

        [HttpGet("name/{name}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetByName(string name)
        {
            var cars = await _carService.BrowseAsync(name);
            return Json(cars);
        }

        // api/cars/id/{id}/rentals - show the rental of a car with given id
        [HttpGet("id/{id}/rentals")]
        public async Task<IActionResult> GetCarRentals(Guid id)
        {
            var rentals = await _rentalService.GetForCarAsync(id);
            if(rentals == null)
            {
                return NotFound();
            }
            return Json(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCarDto car)
        {
            var id = Guid.NewGuid();
            await _carService.CreateAsync(id, car.Name, car.Description, car.PricePerDay, car.PricePerHour);
            return Created($"api/cars/id/{id}", null);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UpdateCarDto car)
        {
            await _carService.UpdateAsync(id, car.Name, car.Description, car.PricePerDay, car.PricePerHour);
            return NoContent();
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _carService.DeleteAsync(id);
            return NoContent();
        }
    }
}
