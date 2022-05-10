using System.Collections.Generic;
using Framework.Application;

namespace DM.Infrastructure.Configure
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "تخفیفات مشتریان", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermission.CustomerList, "مشاهده لیست"),
                        new PermissionDto(DiscountPermission.CustomerSearch, "جستوجو"),
                        new PermissionDto(DiscountPermission.CustomerCreate, "ایجاد"),
                        new PermissionDto(DiscountPermission.CustomerEdit, "ویرایش"),
                    }
                },
                {
                    "تخفیفات همکاران", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermission.ColleagueList, "مشاهه لیست"),
                        new PermissionDto(DiscountPermission.ColleagueSearch, "جستوجو"),
                        new PermissionDto(DiscountPermission.ColleagueCreate, "ایجاد"),
                        new PermissionDto(DiscountPermission.ColleagueEdit, "ویرایش"),
                        new PermissionDto(DiscountPermission.ColleagueRemove, "حذف"),
                        new PermissionDto(DiscountPermission.ColleagueRestore, "موجود کردن"),
                    }
                },
            };
        }
    }
}
