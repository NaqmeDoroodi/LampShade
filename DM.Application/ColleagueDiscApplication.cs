using System.Collections.Generic;
using DM.Application.Contracts.Colleague;
using DM.Application.Contracts.Colleague.Models;
using DM.Domain.ColleagueAgg;
using Framework.Application;

namespace DM.Application
{
    public class ColleagueDiscApplication : IColleagueDiscApplication
    {
        #region inj

        private readonly IColleagueDiscRepository _repository;

        public ColleagueDiscApplication(IColleagueDiscRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public OperationResult Create(CreateColleagueDisc disc)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.ProductId == disc.ProductId && x.DiscRate == disc.DiscRate))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var newDisc = new ColleagueDisc(disc.ProductId, disc.DiscRate);

            _repository.Add(newDisc);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDisc disc)
        {
            var operation = new OperationResult();
            var discToEdit = _repository.Get(disc.Id);

            if (discToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.ProductId == disc.ProductId && x.DiscRate == disc.DiscRate && x.Id != disc.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            discToEdit.Edit(disc.ProductId, disc.DiscRate);

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var disc = _repository.Get(id);

            if (disc == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            disc.Remove();

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var disc = _repository.Get(id);

            if (disc == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            disc.Restore();

            _repository.Save();
            return operation.Succeeded();
        }

        public EditColleagueDisc GetDiscDetails(long id)
        {
            return _repository.GetDiscDetails(id);
        }

        public List<ColleagueDiscViewModel> Search(ColleagueDiscSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
