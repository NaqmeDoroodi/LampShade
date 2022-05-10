using System.Collections.Generic;
using AM.Application.Contract.Account;
using AM.Application.Contract.Account.Models;
using AM.Application.Contract.Role;
using AM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IAccountApplication _accountService;
        private readonly IRoleApplication _roleService;
        public IndexModel(IAccountApplication accountService, IRoleApplication roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        #endregion

        public List<AccountViewModel> Accounts { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public SelectList Roles { get; set; }

        [TempData] public string Message { get; set; }


        [NeedsPermission(AccountPermission.AccountList)]
        public void OnGet(AccountSearchModel model)
        {
            Roles = new SelectList(_roleService.GetRoles(), "Id", "Name");
            Accounts = _accountService.Search(model);
        }


        [NeedsPermission(AccountPermission.AccountRegister)]
        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleService.GetRoles()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(AccountPermission.AccountRegister)]
        public JsonResult OnPostCreate(RegisterAccount account)
        {
            var result = _accountService.Register(account);
            return new JsonResult(result);
        }


        [NeedsPermission(AccountPermission.AccountEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var account = _accountService.GetAccountDetails(id);
            account.Roles = _roleService.GetRoles();
            return Partial("./Edit", account);
        }


        [NeedsPermission(AccountPermission.AccountEdit)]
        public JsonResult OnPostEdit(EditAccount account)
        {
            var result = _accountService.Edit(account);
            return new JsonResult(result);
        }


        [NeedsPermission(AccountPermission.AccountChangPassword)]
        public IActionResult OnGetChangePassword(long id)
        {
            var account = new ChangePassword {Id = id};
            return Partial("./ChangePassword", account);
        }


        [NeedsPermission(AccountPermission.AccountChangPassword)]
        public JsonResult OnPostChangePassword(ChangePassword account)
        {
            var result = _accountService.ChangePassword(account);
            return new JsonResult(result);
        }
    }
}