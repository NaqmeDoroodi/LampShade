using System;
using System.Collections.Generic;
using System.Linq;
using BM.Infrastructure.EFCore;
using CM.Infrastructure.EFCore;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;
using Query.Contracts.Comment;

namespace Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        #region inj

        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        #endregion


        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Img = x.Img,
                    ImgAlt = x.ImgAlt,
                    ImgTitle = x.ImgTitle,
                    ShortDesc = x.ShortDesc,
                    Desc = x.Desc,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDesc = x.MetaDesc,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                }).FirstOrDefault(x => x.Slug == slug);

            if (article != null)
                article.KeywordList = article.Keywords.Split(",").ToList();


            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.OwnerType == CommentType.Article)
                .Where(x => x.OwnerId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            article.Comments = comments;

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Img = x.Img,
                    ImgAlt = x.ImgAlt,
                    ImgTitle = x.ImgTitle,
                    ShortDesc = x.ShortDesc,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                }).ToList();
        }
    }
}