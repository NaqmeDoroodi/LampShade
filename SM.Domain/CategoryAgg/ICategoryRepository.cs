using System.Collections.Generic;
using Framework.Domain;
using SM.Application.Contract.Category.Models;

namespace SM.Domain.CategoryAgg
{
    public interface ICategoryRepository : IRepository<long, Category>
    {
        List<CategoryViewModel> GetCategories();
        string GetCategorySlug(long id);
        List<CategoryViewModel> Search(CategorySearchModel category);
        EditCategory GetDetails(long id);
    }
}