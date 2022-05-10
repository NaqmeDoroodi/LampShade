using DM.Application;
using DM.Application.Contracts.Colleague;
using DM.Application.Contracts.Customer;
using DM.Domain.ColleagueAgg;
using DM.Domain.CustomerAgg;
using DM.Infrastructure.EFCore;
using DM.Infrastructure.EFCore.Repositories;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure.Configure
{
    public class DiscountBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<ICustomerDiscApplication, CustomerDiscApplication>();
            services.AddTransient<ICustomerDiscRepository, CustomerDiscRepository>();

            services.AddTransient<IColleagueDiscApplication, ColleagueDiscApplication>();
            services.AddTransient<IColleagueDiscRepository, ColleagueDiscRepository>();


            services.AddTransient<IPermissionExposer, DiscountPermissionExposer>();


            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
