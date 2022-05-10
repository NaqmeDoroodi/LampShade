using AM.Application.Contract.Account;
using AM.Application.Contract.Account.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        #region inj

        private readonly IAccountApplication _service;

        public AccountModel(IAccountApplication service)
        {
            _service = service;
        }

        #endregion

        [TempData] public string LoginMessage { get; set; }
        [TempData] public string SigninMessage { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPostLogin(Login account)
        {
            var result = _service.Login(account);

            if (result.IsSuccessful)
                return RedirectToPage("/Index");

            LoginMessage = result.Message;
            return RedirectToPage("/Account");
        }


        public IActionResult OnGetLogout()
        {
            _service.Logout();
            return RedirectToPage("/Index");
        }


        public IActionResult OnPostRegister(RegisterAccount account)
        {
            var signinResult = _service.Register(account);
            if (signinResult.IsSuccessful)
            {
                var accountToLogin = new Login {UserName = account.Username, Password = account.Password};
                var loginResult = _service.Login(accountToLogin);
                if (loginResult.IsSuccessful)
                    return RedirectToPage("/Index");
                SigninMessage = loginResult.Message;
                return RedirectToPage("/Account");
            }

            SigninMessage = signinResult.Message;
            return RedirectToPage("/Account");
        }
    }
}