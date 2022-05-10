using Framework.Application;
using IM.Application;
using IM.Application.Contracts.Inventory;
using IM.Domain.InventoryAgg;
using IM.Infrastructure.EFCore;
using IM.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.Inventory;
using Query.Query;

namespace IM.Infrastructure.Configure
{
    public class InventoryBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
