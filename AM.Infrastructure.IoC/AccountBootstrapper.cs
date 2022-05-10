using AM.Application;
using AM.Application.Contract.Account;
using AM.Application.Contract.Role;
using AM.Domain.AccountAgg;
using AM.Domain.RoleAgg;
using AM.Infrastructure.EFCore;
using AM.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Infrastructure.IoC
{
    public class AccountBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
