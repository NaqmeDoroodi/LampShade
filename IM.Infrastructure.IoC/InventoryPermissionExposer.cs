using System.Collections.Generic;
using Framework.Application;

namespace IM.Infrastructure.Configure
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "انبار", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermission.InventoryList, "مشاهده لیست"),
                        new PermissionDto(InventoryPermission.InventorySearch, "جستوجو"),
                        new PermissionDto(InventoryPermission.InventoryCreate, "ایجاد"),
                        new PermissionDto(InventoryPermission.InventoryEdit, "ویرایش"),
                        new PermissionDto(InventoryPermission.InventoryIncrease, "افزایش موجودی"),
                        new PermissionDto(InventoryPermission.InventoryReduce, "کاهش موجودی"),
                        new PermissionDto(InventoryPermission.InventoryLog, "مشاهده لاگ"),
                    }
                },
            };
        }
    }
}
