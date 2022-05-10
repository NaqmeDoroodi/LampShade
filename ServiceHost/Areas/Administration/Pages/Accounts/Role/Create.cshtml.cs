using System.Collections.Generic;
using AM.Application.Contract.Role;
using AM.Application.Contract.Role.Models;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        #region inj

        private readonly IRoleApplication _roleService;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public CreateModel(IRoleApplication roleService, IEnumerable<IPermissionExposer> exposers)
        {
            _roleService = roleService;
            _exposers = exposers;
        }

        #endregion

        public CreateRole Role;
        public List<SelectListItem> Permissions = new();


        public void OnGet()
        {
            foreach (var exposer in _exposers)
            {
                var dictionary = exposer.Expose();
                foreach (var (key, value) in dictionary)
                {
                    var group = new SelectListGroup {Name = key};
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString()) {Group = group};
                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost(CreateRole role)
        {
            var result = _roleService.Create(role);
            return RedirectToPage("Index");
        }
    }
}