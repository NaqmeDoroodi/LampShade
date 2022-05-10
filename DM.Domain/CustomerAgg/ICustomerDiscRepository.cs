using System.Collections.Generic;
using DM.Application.Contracts.Customer.Models;
using Framework.Domain;

namespace DM.Domain.CustomerAgg
{
    public interface ICustomerDiscRepository : IRepository<long, CustomerDisc>
    {
        EditCustomerDisc GetDiscDetails(long id);
        List<CustomerDiscViewModel> Search(CustomerDiscSearchModel disc);
    }
}
