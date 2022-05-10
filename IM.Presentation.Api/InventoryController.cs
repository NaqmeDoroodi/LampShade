using System.Collections.Generic;
using IM.Application.Contracts.Inventory;
using IM.Application.Contracts.Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Inventory;

namespace IM.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        #region inj

        private readonly IInventoryApplication _application;
        private readonly IInventoryQuery _query;
        public InventoryController(IInventoryApplication application, IInventoryQuery query)
        {
            _application = application;
            _query = query;
        }

        #endregion


        [HttpGet("{id}")]
        public List<OperationViewModel> GetLogsBy(long id)
        {
            return _application.GetOperationsLog(id);
        }


        [HttpPost]
        public StockStatus CheckStock(IsStock command)
        {
            return _query.CheckStock(command);
        }
    }
}
