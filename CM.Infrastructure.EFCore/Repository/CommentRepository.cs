using System.Collections.Generic;
using System.Linq;
using CM.Application.Contract.Comment.Models;
using CM.Domain.CommentAgg;
using Framework.Application;
using Framework.Infrastructure;

namespace CM.Infrastructure.EFCore.Repository
{
    public class CommentRepository : BaseRepository<long,Comment>,ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled,
                OwnerId = x.OwnerId,
                OwnerType = x.OwnerType,
                CommentDate = x.CreationDate.ToFarsi(),
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if(!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
