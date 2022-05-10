using System.Collections.Generic;
using DM.Application.Contracts.Customer;
using DM.Application.Contracts.Customer.Models;
using DM.Domain.CustomerAgg;
using Framework.Application;

namespace DM.Application
{
    public class CustomerDiscApplication : ICustomerDiscApplication
    {
        #region inj

        private readonly ICustomerDiscRepository _repository;

        public CustomerDiscApplication(ICustomerDiscRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public OperationResult CreateDiscount(CreateCustomerDisc disc)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.ProductId == disc.ProductId && x.DiscRate == disc.DiscRate))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var startDate = disc.StartDate.ToGeorgianDateTime();
            var endDate = disc.EndDate.ToGeorgianDateTime();
            var newDisc = new CustomerDisc(disc.ProductId, disc.DiscRate, startDate, endDate, disc.Reason);

            _repository.Add(newDisc);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult EditDiscount(EditCustomerDisc disc)
        {
            var operation = new OperationResult();
            var discToEdit = _repository.Get(disc.Id);

            if (discToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.ProductId == disc.ProductId && x.DiscRate == disc.DiscRate && x.Id != disc.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var startDate = disc.StartDate.ToGeorgianDateTime();
            var endDate = disc.EndDate.ToGeorgianDateTime();
            discToEdit.Edit(disc.ProductId, disc.DiscRate, startDate, endDate, disc.Reason);

            _repository.Save();
            return operation.Succeeded();
        }

        public EditCustomerDisc GetDiscDetails(long id)
        {
            return _repository.GetDiscDetails(id);
        }

        public List<CustomerDiscViewModel> Search(CustomerDiscSearchModel disc)
        {
            return _repository.Search(disc);
        }
    }
}
