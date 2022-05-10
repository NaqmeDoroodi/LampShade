using System.Collections.Generic;
using Framework.Application;

namespace BM.Infrastructure.Configure
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermission.ArticleList, "مشاهده لیست"),
                        new PermissionDto(BlogPermission.ArticleSearch, "جستوجو"),
                        new PermissionDto(BlogPermission.ArticleCreate, "ایجاد"),
                        new PermissionDto(BlogPermission.ArticleEdit, "ویرایش"),
                    }
                },
                {
                    "گروه مقالات", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermission.CategoryList, "مشاهه لیست"),
                        new PermissionDto(BlogPermission.CategorySearch, "جستوجو"),
                        new PermissionDto(BlogPermission.CategoryCreate, "ایجاد"),
                        new PermissionDto(BlogPermission.CategoryEdit, "ویرایش"),
                    }
                },
            };
        }
    }
}