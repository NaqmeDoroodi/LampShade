using System.Collections.Generic;
using Framework.Application;

namespace CM.Infrastructure.Configure
{
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "کامنت‌ها", new List<PermissionDto>
                    {
                        new PermissionDto(CommentPermission.CommentList, "مشاهده لیست"),
                        new PermissionDto(CommentPermission.CommentSearch, "جستوجو"),
                        new PermissionDto(CommentPermission.CommentCancel, "کنسل"),
                        new PermissionDto(CommentPermission.CommentConfirm, "تایید"),
                    }
                },
            };
        }
    }
}
