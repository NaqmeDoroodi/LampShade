using System.Collections.Generic;

namespace Query.Contracts.Category
{
    public interface ICategoryQuery
    {
        List<CategoryQueryModel> GetCategoriesForMenu();
        List<CategoryQueryModel> GetCategories();
        List<CategoryQueryModel> GetCategoriesWithProducts();
        CategoryQueryModel GetCategoryWithProducts(string slug);
    }
}
