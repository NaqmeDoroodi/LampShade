using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Query.Contracts.Article;

namespace BM.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleQuery _query;
        public ArticleController(IArticleQuery query)
        {
            _query = query;
        }


        [HttpGet]
        public List<ArticleQueryModel> GetArticles()
        {
            return _query.GetLatestArticles();
        }
    }
}
