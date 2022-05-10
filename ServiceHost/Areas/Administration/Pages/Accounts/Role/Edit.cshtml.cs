using System.Collections.Generic;
using System.Linq;
using AM.Application.Contract.Role;
using AM.Application.Contract.Role.Models;
using AM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        #region inj

        private readonly IRoleApplication _roleService;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public EditModel(IRoleApplication roleService, IEnumerable<IPermissionExposer> exposers)
        {
            _roleService = roleService;
            _exposers = exposers;
        }

        #endregion

        public EditRole Role;
        public List<SelectListItem> Permissions = new();


        [NeedsPermission(AccountPermission.RoleEdit)]
        public void OnGet(int id)
        {
            Role = _roleService.GetDetailsRole(id);
            foreach (var exposer in _exposers)
            {
                var dictionary = exposer.Expose();
                foreach (var (key, value) in dictionary)
                {
                    var group = new SelectListGroup {Name = key};
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString()) {Group = group};
                        if (Role.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;
                        Permissions.Add(item);
                    }
                }
            }
        }


        [NeedsPermission(AccountPermission.RoleEdit)]
        public RedirectToPageResult OnPost(EditRole role)
        {
            var result = _roleService.Edit(role);
            return RedirectToPage("Index");
        }
    }
}