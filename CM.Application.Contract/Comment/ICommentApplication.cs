using System.Collections.Generic;
using CM.Application.Contract.Comment.Models;
using Framework.Application;

namespace CM.Application.Contract.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment comment);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
