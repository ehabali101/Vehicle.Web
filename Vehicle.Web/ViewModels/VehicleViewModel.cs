using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Web.ViewModels
{
    public class VehicleViewModel
    {
        public string VehicleId { get; set; }

        public string VehicleStatus { get; set; }

        public string RegistrationNumber { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }
  
        public string CustomerAddress { get; set; }
    }
}
