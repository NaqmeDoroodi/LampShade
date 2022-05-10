using System.Collections.Generic;
using Framework.Application;
using Framework.Infrastructure;
using IM.Application.Contracts.Inventory;
using IM.Application.Contracts.Inventory.Models;
using IM.Infrastructure.Configure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Product;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    [Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IInventoryApplication _inventoryService;
        private readonly IProductApplication _productService;

        public IndexModel(IInventoryApplication inventoryService, IProductApplication productService)
        {
            _inventoryService = inventoryService;
            _productService = productService;
        }

        #endregion

        public List<InventoryViewModel> Inventory { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(InventoryPermission.InventoryList)]
        public void OnGet(InventorySearchModel model)
        {
            Inventory = _inventoryService.Search(model);
            Products = new SelectList(_productService.GetProducts(), "Id", "Name");
        }


        [NeedsPermission(InventoryPermission.InventoryCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productService.GetProducts()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(InventoryPermission.InventoryCreate)]
        public JsonResult OnPostCreate(CreateInventory inventory)
        {
            var result = _inventoryService.Create(inventory);
            return new JsonResult(result);
        }


        [NeedsPermission(InventoryPermission.InventoryEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryService.GetInventoryDetails(id);
            inventory.Products = _productService.GetProducts();
            return Partial("./Edit", inventory);
        }


        [NeedsPermission(InventoryPermission.InventoryEdit)]
        public JsonResult OnPostEdit(EditInventory inventory)
        {
            var result = _inventoryService.Edit(inventory);
            return new JsonResult(result);
        }


        [NeedsPermission(InventoryPermission.InventoryIncrease)]
        public IActionResult OnGetIncrease(long id)
        {
            var inventory = new IncreaseInventory()
            {
                InventoryId = id,
            };
            return Partial("./Increase", inventory);
        }


        [NeedsPermission(InventoryPermission.InventoryIncrease)]
        public JsonResult OnPostIncrease(IncreaseInventory inventory)
        {
            var result = _inventoryService.Increase(inventory);
            return new JsonResult(result);
        }


        [NeedsPermission(InventoryPermission.InventoryReduce)]
        public IActionResult OnGetReduce(long id)
        {
            var inventory = new ReduceInventory()
            {
                InventoryId = id,
            };
            return Partial("./Reduce", inventory);
        }


        [NeedsPermission(InventoryPermission.InventoryReduce)]
        public JsonResult OnPostReduce(ReduceInventory inventory)
        {
            var result = _inventoryService.Reduce(inventory);
            return new JsonResult(result);
        }


        [NeedsPermission(InventoryPermission.InventoryLog)]
        public IActionResult OnGetLog(long id)
        {
            var logs = _inventoryService.GetOperationsLog(id);
            return Partial("./OperationLog", logs);
        }
    }
}