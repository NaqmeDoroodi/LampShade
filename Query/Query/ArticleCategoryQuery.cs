using System.Collections.Generic;
using System.Linq;
using BM.Domain.ArticleAgg;
using BM.Infrastructure.EFCore;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;

namespace Query.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        #region inj

        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        #endregion


        public ArticleCategoryQueryModel GetCategoryWithArticles(string slug)
        {
            var category = _context.ArticleCategories.Select(x=>new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Slug = x.Slug,
                Desc = x.Desc,
                MetaDesc = x.MetaDesc,
                Keywords = x.Keywords,
                CanonicalAddress = x.CanonicalAddress,
                Articles = MapArticle(x.Articles),
            }).FirstOrDefault(x => x.Slug == slug);

            if(category != null && !string.IsNullOrWhiteSpace(category.Keywords)) category.KeywordsList = category.Keywords.Split(",").ToList();

            return category;
        }

        public List<ArticleCategoryQueryModel> GetCategories()
        {
            return _context.ArticleCategories.Include(x => x.Articles).Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Slug = x.Slug,
                ArticleCnt = x.Articles.Count,
            }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetCategoryForMenu()
        {
            return _context.ArticleCategories.Select(x=>new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug,
                Articles = x.Articles.Select(x => new ArticleQueryModel { Title = x.Title, Slug = x.Slug }).ToList(),
            }).ToList();
        }

        private static List<ArticleQueryModel> MapArticle(List<Article> articles)
        {
            return articles.Select(x=>new ArticleQueryModel
            {
                Title = x.Title,
                Slug=x.Slug,
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                ShortDesc = x.ShortDesc,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}