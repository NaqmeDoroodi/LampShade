using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Article;

namespace ServiceHost.ViewComponents
{
    public class LatestBlogsViewComponent : ViewComponent
    {
        private readonly IArticleQuery _query;
        public LatestBlogsViewComponent(IArticleQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _query.GetLatestArticles();
            return View(articles);
        }
    }
}
