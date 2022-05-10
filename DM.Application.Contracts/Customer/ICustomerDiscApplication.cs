using System.Collections.Generic;
using DM.Application.Contracts.Customer.Models;
using Framework.Application;

namespace DM.Application.Contracts.Customer
{
    public interface ICustomerDiscApplication
    {
        OperationResult CreateDiscount(CreateCustomerDisc disc);
        OperationResult EditDiscount(EditCustomerDisc disc);
        EditCustomerDisc GetDiscDetails(long id);
        List<CustomerDiscViewModel> Search(CustomerDiscSearchModel disc);
    }
}
