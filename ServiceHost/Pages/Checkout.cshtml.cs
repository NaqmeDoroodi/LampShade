using System.Collections.Generic;
using System.Linq;
using Framework.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using Query.Contracts.Order;
using Query.Contracts.Product;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        #region inj

        private readonly ICartQuery _query;
        private readonly ICartService _service;
        private readonly IProductQuery _productQuery;
        private readonly IConfiguration _configuration;
        private readonly IOrderApplication _orderApplication;

        public CheckoutModel(ICartQuery query, ICartService service, IProductQuery productQuery,
            IOrderApplication orderApplication, IConfiguration configuration)
        {
            _query = query;
            _service = service;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _configuration = configuration;
            Cart = new Cart();
        }

        #endregion

        private const string CookieName = "cart-items";
        public Cart Cart;

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var items = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in items) item.CalcItemTotalPrice();

            Cart = _query.ComputeCart(items);
            _service.Set(Cart);
        }

        public IActionResult OnPostPay(int paymentMethodId)
        {
            var cart = _service.Get();
            cart.SetPaymentMethod(paymentMethodId);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);

            if (paymentMethodId == 1)
            {
                var siteUrl = _configuration.GetSection("Payment")["SiteUrl"];
                var paymentResponse = new ZarinpalSandbox.Payment(int.Parse(cart.PayPrice.ToString())).PaymentRequest("خرید آنلاین از سایت", $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}");

                return Redirect(paymentResponse.Result.Link);
            }
            else
            {
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد. ", ""));
            }
        }


        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            //var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            var verificationResponse = new ZarinpalSandbox.Payment(int.Parse(orderAmount.ToString())).Verification(authority);

            var result = new PaymentResult();

            if (status == "OK" && verificationResponse.Result.Status == 100)
            {
                var trackingNum = _orderApplication.PaymentSucceeded(oId, verificationResponse.Result.RefId);
                Response.Cookies.Delete(CookieName);
                result = result.Succeeded(ApplicationMessage.PaymentSucceeded, trackingNum);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(ApplicationMessage.PaymentFailed);
            return RedirectToPage("/PaymentResult", result);
        }
    }
}