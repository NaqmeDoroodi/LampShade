using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.Category;
using Query.Contracts.Product;
using Query.Contracts.Slide;
using Query.Query;
using SM.Application;
using SM.Application.Contract.Category;
using SM.Application.Contract.Image;
using SM.Application.Contract.Order;
using SM.Application.Contract.Product;
using SM.Application.Contract.Slide;
using SM.Domain.CategoryAgg;
using SM.Domain.ImageAgg;
using SM.Domain.OrderAgg;
using SM.Domain.ProductAgg;
using SM.Domain.Services;
using SM.Domain.SlideAgg;
using SM.Infrastructure.AccountAcl;
using SM.Infrastructure.EFCore;
using SM.Infrastructure.EFCore.Repositories;
using SM.Infrastructure.InventoryAcl;

namespace SM.Infrastructure.Configure
{
    public class ShopBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<ICategoryApplication, CategoryApplication>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IImageApplication, ImageApplication>();
            services.AddTransient<IImageRepository, ImageRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();


            services.AddSingleton<ICartService, CartService>();
           
            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();
            services.AddTransient<IShopAccountAcl, ShopAccountAcl>();


            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<ICategoryQuery, CategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
