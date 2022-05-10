using System.Collections.Generic;
using Framework.Application;

namespace AM.Application.Contract.Role.Models
{
    public class EditRole : CreateRole
    {
        public int Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}