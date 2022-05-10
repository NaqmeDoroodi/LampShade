using System.Collections.Generic;
using CM.Application.Contract.Comment.Models;
using Framework.Domain;

namespace CM.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
