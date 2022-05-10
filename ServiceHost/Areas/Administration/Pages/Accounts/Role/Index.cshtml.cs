using System.Collections.Generic;
using AM.Application.Contract.Role;
using AM.Application.Contract.Role.Models;
using AM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IRoleApplication _roleService;

        public IndexModel(IRoleApplication roleService)
        {
            _roleService = roleService;
        }

        #endregion

        public List<RoleViewModel> Roles { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(AccountPermission.RoleList)]
        public void OnGet()
        {
            Roles = _roleService.GetRoles();
        }
    }
}