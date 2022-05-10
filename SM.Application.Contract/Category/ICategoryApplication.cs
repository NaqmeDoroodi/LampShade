using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Category.Models;

namespace SM.Application.Contract.Category
{
    public interface ICategoryApplication
    {
        List<CategoryViewModel> GetCategories();
        string GetSlugBy(long id);
        List<CategoryViewModel> Search(CategorySearchModel category);
        EditCategory GetCategory(long id);
        OperationResult Create(CreateCategory category);
        OperationResult Edit(EditCategory category);
    }
}