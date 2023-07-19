using CarRental.Infrastructure.DTO.Cars;
using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;

namespace CarRental.APP.Data
{
    public class CarsService
    {
        private CarDto[]? _carsDto;
        private HttpClientHandler _clientHandler;
        private HttpClient _carClient;
        public event Action? CarsChanged;

        public CarDto[] CarsDto
        {
            get => _carsDto;
            set
            {
                _carsDto = value;
                CarsChanged?.Invoke();
            }

        }

        public async Task AddCar(CreateCarDto createCar)
        {
            await _carClient.PostAsJsonAsync("https://localhost:7255/api/cars", createCar);
            await GetCars();
        }
        public async Task AddRental(CreateRentalDto createRental)
        {
            await _carClient.PostAsJsonAsync("https://localhost:7255/api/account/rentals", createRental);
        }
        public async Task DeleteCar(Guid id)
        {
            await _carClient.DeleteAsync($"https://localhost:7255/api/cars/id/{id}");
            await GetCars();
        }
        public async Task GetCars()
        {
            _carsDto = await _carClient.GetFromJsonAsync<CarDto[]>("https://localhost:7255/api/cars/");
        }
        public CarsService()
        {
            _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _carClient = new HttpClient(_clientHandler);
        }
    }
}
