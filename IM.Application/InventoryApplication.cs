using System.Collections.Generic;
using Framework.Application;
using IM.Application.Contracts.Inventory;
using IM.Application.Contracts.Inventory.Models;
using IM.Domain.InventoryAgg;

namespace IM.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        #region inj

        private readonly IInventoryRepository _repository;
        private readonly IAuthHelper _authHelper;
        public InventoryApplication(IInventoryRepository repository, IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }

        #endregion


        public OperationResult Create(CreateInventory inventory)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(x => x.ProductId == inventory.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var newInventory = new Inventory(inventory.ProductId, inventory.UnitePrice);

            _repository.Add(newInventory);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory inventory)
        {
            var operation = new OperationResult();
            var inventoryToEdit = _repository.Get(inventory.Id);

            if (inventoryToEdit == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_repository.DoesExist(x => x.ProductId == inventory.ProductId && x.Id != inventory.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            inventoryToEdit.Edit(inventory.ProductId, inventory.UnitePrice);

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory inventory)
        {
            var operation = new OperationResult();
            var inventoryToIncrease = _repository.Get(inventory.InventoryId);

            if (inventoryToIncrease == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var operatorId = _authHelper.AccountId();
            inventoryToIncrease.Increase(inventory.Count, inventory.Desc, operatorId);

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory inventory)
        {
            var operation = new OperationResult();
            var inventoryToReduce = _repository.Get(inventory.InventoryId);

            if (inventoryToReduce == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var operatorId = _authHelper.AccountId();
            inventoryToReduce.Reduce(inventory.Count, inventory.Desc, operatorId, 0); //this method only called by admin so orderId is always 0

            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> inventories)
        {
            var operation = new OperationResult();
            var operatorId = _authHelper.AccountId();

            foreach (var inventory in inventories)
            {
                var inventoryToReduce = _repository.GetInventory(inventory.ProductId);
                inventoryToReduce.Reduce(inventory.Count, inventory.Desc, operatorId, inventory.OrderId);
            }

            _repository.Save();
            return operation.Succeeded();
        }

        public EditInventory GetInventoryDetails(long id)
        {
            return _repository.GetInventoryDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public List<OperationViewModel> GetOperationsLog(long inventoryId)
        {
            return _repository.GetOperationsLog(inventoryId);
        }

    }
}