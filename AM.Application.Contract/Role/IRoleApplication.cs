using System.Collections.Generic;
using AM.Application.Contract.Role.Models;
using Framework.Application;

namespace AM.Application.Contract.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole role);
        OperationResult Edit(EditRole role);
        EditRole GetDetailsRole(int id);
        List<RoleViewModel> GetRoles();
    }
}