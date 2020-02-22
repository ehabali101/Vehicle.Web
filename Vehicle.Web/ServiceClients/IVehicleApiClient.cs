using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleResources;

namespace Vehicle.Web.ServiceClients
{
    public interface IVehicleApiClient
    {
        Task<IEnumerable<VehicleResource>> GetVehicles();
        void UpdateStatus(VehicleResource vehicle);
        Task<IEnumerable<VehicleOwnersResource>> GetVehicleOwners();
    }
}
