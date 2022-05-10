using System.Collections.Generic;
using Framework.Application;

namespace AM.Infrastructure.Configure
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "کاربران", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermission.AccountList, "مشاهده لیست"),
                        new PermissionDto(AccountPermission.AccountSearch, "جستوجو"),
                        new PermissionDto(AccountPermission.AccountRegister, "ایجاد"),
                        new PermissionDto(AccountPermission.AccountEdit, "ویرایش"),
                        new PermissionDto(AccountPermission.AccountChangPassword, "تغییر رمز عبور"),
                    }
                },
                {
                    "نقش‌ها", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermission.RoleList, "مشاهه لیست"),
                        new PermissionDto(AccountPermission.RoleCreate, "ایجاد"),
                        new PermissionDto(AccountPermission.RoleEdit, "ویرایش"),
                    }
                },
            };
        }
    }
}