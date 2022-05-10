using CM.Application;
using CM.Application.Contract.Comment;
using CM.Domain.CommentAgg;
using CM.Infrastructure.EFCore;
using CM.Infrastructure.EFCore.Repository;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CM.Infrastructure.Configure
{
    public class CommentBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStr)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IPermissionExposer, CommentPermissionExposer>();


            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionStr));
        }
    }
}
