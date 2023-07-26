using CarRental.Infrastructure.DTO;
using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;

namespace CarRental.APP.Data
{
    public class AccountService
    {
        private TokenDto _token;
        private HttpClient _accountClient;
        private RentalDto[]? _rentalsDto;


        public event Action OnRentalsChanged;
        public TokenDto Token { get { return _token; } }
        public HttpClient AccountClient { get { return _accountClient; } }
        public RentalDto[]? RentalsDto { get { return _rentalsDto; } }

        public AccountService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _accountClient = new HttpClient(clientHandler);
        }
        public async Task Register(Register register)
        {
            AccountClient.PostAsJsonAsync("https://localhost:7255/[api/account]/register", register);
        }
        public async Task LogOut()
        {
            _token = null;
        }
        public async Task DeleteRental(Guid id)
        {
            await AccountClient.DeleteAsync($"https://localhost:7255/[api/account]/rentals/id/{id}");
            await FetchRentalsAsync();
        }
        public async Task<bool> IsAdmin()
        {
            if (Token != null)
            {
                return Token.Role.ToLower().Equals("admin");
            }
            return false;
        }
        public async Task SetAccountAsync(TokenDto account)
        {
            _token = account;
            _accountClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.JWT);
        }
        /*public async Task SetAccountHttpClienttAsync(HttpClient client)
        {
            _accountClient = client;
        }*/
        public async Task SetRentalsDtoAsync(RentalDto[] rentalsDto)
        {
            _rentalsDto = rentalsDto;
            OnRentalsChanged?.Invoke();
        }
        public async Task FetchRentalsAsync()
        {
            if (Token != null)
            {
                SetRentalsDtoAsync(await AccountClient.GetFromJsonAsync<RentalDto[]>($"https://localhost:7255/[api/account]/rentals"));
            }
        }

    }
}
