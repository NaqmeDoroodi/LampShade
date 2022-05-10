using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using Query.Contracts.Product;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        #region inj

        private readonly IProductQuery _query;

        public CartModel(IProductQuery query)
        {
            Items = new List<CartItem>();
            _query = query;
        }

        #endregion

        private const string CookieName = "cart-items";
        public List<CartItem> Items { get; set; }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var items = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in items) item.CalcItemTotalPrice();

            Items = _query.CheckInventoryStatus(items);
        }

        public IActionResult OnGetRemove(int id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var items = serializer.Deserialize<List<CartItem>>(value);

            var itemToRemove = items.FirstOrDefault(x => x.Id == id);

            items.Remove(itemToRemove);

            var options = new CookieOptions {Expires = DateTime.Now.AddDays(10)};
            Response.Cookies.Append(CookieName,serializer.Serialize(items),options);

            return RedirectToPage("/Cart");
        }


        public IActionResult OnGetCheckout()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var items = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in items) item.CalcItemTotalPrice();

            Items = _query.CheckInventoryStatus(items);

            return RedirectToPage(Items.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");
        }
    }
}