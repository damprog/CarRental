using CarRental.Infrastructure.DTO.Rentals;
using CarRental.Infrastructure.DTO.Users;

namespace CarRental.APP.Data
{
    public class AccountService
    {
        private AccountDto _account;
        private HttpClient _accountClient;
        private RentalDto[]? _rentalsDto;

        public event Action OnRentalsChanged;
        public AccountDto Account { get { return _account; } }
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
            AccountClient.PostAsJsonAsync("https://localhost:7255/api/account/register", register);
        }
        public async Task LogOut()
        {
            _account = null;
        }
        public async Task DeleteRental(Guid id)
        {
            await AccountClient.DeleteAsync($"https://localhost:7255/api/account/rentals/id/{id}");
            await FetchRentalsAsync();
        }
        public async Task<bool> IsAdmin()
        {
            if (Account != null)
            {
                return Account.Role.ToLower().Equals("admin");
            }
            return false;
        }
        public async Task SetAccountAsync(AccountDto account)
        {
            _account = account;
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
            if (Account != null)
            {
                SetRentalsDtoAsync(await AccountClient.GetFromJsonAsync<RentalDto[]>($"https://localhost:7255/api/account/rentals/{Account.Id}"));
            }
        }

    }
}
