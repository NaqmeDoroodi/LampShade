using System;
using System.Collections.Generic;
using System.Linq;
using BM.Application.Contract.Article.Models;
using BM.Domain.ArticleAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
    {
        #region inj

        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                ShortDesc = x.ShortDesc,
                Desc = x.Desc,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDesc = x.MetaDesc,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles
                .Include(x => x.Category)
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDesc = x.ShortDesc.Substring(0,Math.Min(x.ShortDesc.Length,50)) + "...",
                    Img = x.Img,
                    PublishDate = x.PublishDate.ToFarsi(),
                    CategoryId = x.CategoryId,
                    Category = x.Category.Name,
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}