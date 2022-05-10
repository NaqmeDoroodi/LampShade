using System.Collections.Generic;
using Query.Contracts.ArticleCategory;
using Query.Contracts.Category;

namespace Query
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<CategoryQueryModel> ProductCategories { get; set; }
    }
}
