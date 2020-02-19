using CustomerResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Web.ServiceClients
{
    public interface ICustomerApiClient
    {
        Task<IEnumerable<CustomerResource>> GetCustomers();
    }
}
