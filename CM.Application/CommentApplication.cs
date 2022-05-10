using System.Collections.Generic;
using CM.Application.Contract.Comment;
using CM.Application.Contract.Comment.Models;
using CM.Domain.CommentAgg;
using Framework.Application;

namespace CM.Application
{
    public class CommentApplication : ICommentApplication
    {
        #region inj

        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public OperationResult Add(AddComment comment)
        {
            var operation = new OperationResult();
            var newComment = new Comment(comment.Name, comment.Email, comment.Website, comment.Message, comment.OwnerId,
                comment.OwnerType, comment.ParentId);

            _repository.Add(newComment);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _repository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Confirm();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _repository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Cancel();
            _repository.Save();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}