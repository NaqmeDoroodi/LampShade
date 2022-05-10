using System.Collections.Generic;
using Framework.Application;

namespace SM.Infrastructure.Configure
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ProductList, "مشاهده لیست"),
                        new PermissionDto(ShopPermission.ProductSearch, "جستوجو"),
                        new PermissionDto(ShopPermission.ProductCreate, "ایجاد"),
                        new PermissionDto(ShopPermission.ProductEdit, "ویرایش"),
                    }
                },
                {
                    "گروه محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.CategoryList, "مشاهده لیست"),
                        new PermissionDto(ShopPermission.CategorySearch, "جستوجو"),
                        new PermissionDto(ShopPermission.CategoryCreate, "ایجاد"),
                        new PermissionDto(ShopPermission.CategoryEdit, "ویرایش"),
                    }
                },
                {
                    "عکس‌ها", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ImageList, "مشاهده لیست"),
                        new PermissionDto(ShopPermission.ImageSearch, "جستوجو"),
                        new PermissionDto(ShopPermission.ImageCreate, "ایجاد"),
                        new PermissionDto(ShopPermission.ImageEdit, "ویرایش"),
                        new PermissionDto(ShopPermission.ImageRemove, "حذف"),
                        new PermissionDto(ShopPermission.ImageRestore, "موجود کردن"),
                    }
                },
                {
                    "اسلایدها", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermission.ImageList, "مشاهده لیست"),
                        new PermissionDto(ShopPermission.ImageCreate, "ایجاد"),
                        new PermissionDto(ShopPermission.ImageEdit, "ویرایش"),
                        new PermissionDto(ShopPermission.ImageRemove, "حذف"),
                        new PermissionDto(ShopPermission.ImageRestore, "موجود کردن"),
                    }
                },
            };
        }
    }
}