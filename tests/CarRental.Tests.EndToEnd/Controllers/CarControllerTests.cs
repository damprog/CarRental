using CarRental.Infrastructure.DTO.Cars;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Tests.EndToEnd.Controllers
{
    public class CarControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        // !!!! Somethinkg doesnt work 

        public CarControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<IHostingStartup>()); // Here in tutorial was Startup not IHostingStartup
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task fetching_cars_should_return_not_empty_collection()
        {
            var response = await _client.GetAsync("cars"); // Here i am not sure if cars is proper
            var content = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(content);

            response.StatusCode.ToString().Should().BeEquivalentTo(HttpStatusCode.OK.ToString());
            cars.Should().NotBeEmpty();
        }
    }
}
