using System.Collections.Generic;
using System.Linq;
using Framework.Application;
using Framework.Infrastructure;
using SM.Application.Contract.Category.Models;
using SM.Domain.CategoryAgg;

namespace SM.Infrastructure.EFCore.Repositories
{
    public class CategoryRepository : BaseRepository<long, Category>, ICategoryRepository
    {
        #region inj

        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public List<CategoryViewModel> GetCategories()
        {
            return _context.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public string GetCategorySlug(long id)
        {
            return _context.Categories.Select(x => new {x.Id, x.Slug}).FirstOrDefault(x => x.Id == id)?.Slug;
        }

        public List<CategoryViewModel> Search(CategorySearchModel category)
        {
            var query = _context.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                Img = x.Img,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if (!string.IsNullOrWhiteSpace(category.Name))
                query = query.Where(x => x.Name == category.Name);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditCategory GetDetails(long id)
        {
            return _context.Categories.Select(x => new EditCategory
            {
                Id = x.Id,
                Name = x.Name,
                Desc = x.Desc,
                //Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Keywords = x.Keywords,
                MetaDesc = x.MetaDesc,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}