using System.Collections.Generic;
using AM.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace ServiceHost.Areas.Administration.Pages.Shop.Order
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;
        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        #endregion
        
        public OrderSearchModel SearchModel { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public SelectList Accounts { get; set; }



        public void OnGet(OrderSearchModel model)
        {
            Orders = _orderApplication.Search(model);
            Accounts = new SelectList(_accountApplication.GetAccountsList(), "Id", "Fullname");
        }

        public IActionResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0); //admin accept the order so refId is 0
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetItems(long id)
        {
            var items = _orderApplication.GetItems(id);
            return Partial("Items",items);
        }
    }
}
