using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Web.ServiceClients;
using Vehicle.Web.ViewModels;
using VehicleResources;

namespace Vehicle.Web.Controllers
{
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleApiClient _vehicleApiClient;
        private readonly ICustomerApiClient _customerApiClient;

        public VehicleController(IVehicleApiClient vehicleApiClient, ICustomerApiClient customerApiClient)
        {
            _vehicleApiClient = vehicleApiClient;
            _customerApiClient = customerApiClient;
        }

        [HttpGet]
        [Route("Vehicles")]
        public IEnumerable<VehicleViewModel> GetVehicles()
        {
            return GetVehicleViewModel();
        }

        [HttpGet]
        [Route("Customers")]
        public IEnumerable<CustomerResource> GetCustomers()
        {
            return _customerApiClient.GetCustomers().Result;
        }

        [HttpGet]
        [Route("SyncVehicles")]
        public IEnumerable<VehicleViewModel> SyncVehicles()
        {
            var vehicles = _vehicleApiClient.GetVehicles().Result;
            Random rand = new Random();
            foreach (var vehicle in vehicles)
            {           
                var newStatus = rand.Next(2) == 0? 1 : 2;
                vehicle.Status = (VehicleStatusResource)newStatus;
                _vehicleApiClient.UpdateStatus(vehicle);
            }

            return GetVehicleViewModel();
        }

        private IEnumerable<VehicleViewModel> GetVehicleViewModel()
        {
            var customers = _customerApiClient.GetCustomers().Result;
            var vehicles = _vehicleApiClient.GetVehicles().Result;
            var vehicleOwners = _vehicleApiClient.GetVehicleOwners().Result;

            var monitorList = new List<VehicleViewModel>();

            foreach (var owner in vehicleOwners)
            {
                var customer = customers.FirstOrDefault(c => c.Id == owner.CustomerId);
                var vehicle = vehicles.FirstOrDefault(c => c.Id == owner.VehicleId);

                var model = new VehicleViewModel();
                model.VehicleId = owner.VehicleId;
                model.RegistrationNumber = owner.RegistrationNumber;
                model.CustomerId = owner.CustomerId;
                model.CustomerName = customer.Name;
                model.CustomerAddress = customer.Address;
                model.VehicleStatus = Enum.GetName(typeof(VehicleStatusResource), vehicle.Status);

                monitorList.Add(model);
            }
            return monitorList.ToList();
        }
    }
}