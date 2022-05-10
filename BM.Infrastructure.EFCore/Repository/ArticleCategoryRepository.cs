using System.Collections.Generic;
using System.Linq;
using BM.Application.Contract.Category.Models;
using BM.Domain.CategoryAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : BaseRepository<long, ArticleCategory>, IArticleCategoryRepository
    {
        #region inj

        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        #endregion


        public string GetSlugBy(long id)
        {
            return _context.ArticleCategories.Select(x => new {x.Id, x.Slug,}).FirstOrDefault(x => x.Id == id)?.Slug;
        }

        public EditArticleCategory GetCategoryDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Slug = x.Slug,
                ShowOrder = x.ShowOrder,
                Keywords = x.Keywords,
                MetaDesc = x.MetaDesc,
                CanonicalAddress = x.CanonicalAddress,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> GetCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Desc = x.Desc,
                    Img = x.Img,
                    ShowOrder = x.ShowOrder,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ArticlesCnt = x.Articles.Count,
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.ShowOrder).ToList();
        }
    }
}