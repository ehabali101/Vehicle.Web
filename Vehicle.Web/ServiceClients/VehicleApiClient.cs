using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleResources;

namespace Vehicle.Web.ServiceClients
{
    public class VehicleApiClient : IVehicleApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public VehicleApiClient(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;

            _client.BaseAddress = new Uri(_configuration.GetSection("VehicleApi").GetValue<string>("Url"));
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");
        }

        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var response = await _client.GetAsync(new Uri($"{_client.BaseAddress}/vehicles"));
            return await response.Content.ReadAsAsync<IEnumerable<VehicleResource>>();
        }

        public async Task<VehicleResource> UpdateStatus(VehicleResource vehicle)
        {
            var jsonModel = JsonConvert.SerializeObject(vehicle);
            var response = await _client.PutAsync(new Uri($"{_client.BaseAddress}/vehicles/{vehicle.Id}"),
                new StringContent(jsonModel));

            return await response.Content.ReadAsAsync<VehicleResource>();
        }

        public async Task<IEnumerable<VehicleOwnersResource>> GetVehicleOwners()
        {
            var response = await _client.GetAsync(new Uri($"{_client.BaseAddress}/vehicles/owners"));
            return await response.Content.ReadAsAsync<IEnumerable<VehicleOwnersResource>>();
        }
    }
}
