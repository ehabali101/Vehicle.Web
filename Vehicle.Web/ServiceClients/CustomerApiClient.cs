using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerResources;
using Microsoft.Extensions.Configuration;

namespace Vehicle.Web.ServiceClients
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public CustomerApiClient(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;

            _client.BaseAddress = new Uri(_configuration.GetSection("CustomerApi").GetValue<string>("Url"));
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");
        }

        public async Task<IEnumerable<CustomerResource>> GetCustomers()
        {
            var response = await _client.GetAsync(new Uri($"{_client.BaseAddress}/customers"));
            return await response.Content.ReadAsAsync<IEnumerable<CustomerResource>>();
        }
    }
}
