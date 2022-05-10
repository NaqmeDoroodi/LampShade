using Framework.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentResultModel : PageModel
    {
        public PaymentResult Result { get; set; }
        public void OnGet(PaymentResult result)
        {
            Result = result;
        }
    }
}
