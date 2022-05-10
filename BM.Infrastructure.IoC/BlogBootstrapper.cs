using BM.Application;
using BM.Application.Contract.Article;
using BM.Application.Contract.Category;
using BM.Domain.ArticleAgg;
using BM.Domain.CategoryAgg;
using BM.Infrastructure.EFCore;
using BM.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;
using Query.Query;

namespace BM.Infrastructure.IoC
{
    public class BlogBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
