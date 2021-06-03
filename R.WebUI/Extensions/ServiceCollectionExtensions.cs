using Microsoft.Extensions.DependencyInjection;
using System;
using R.Entities.Constants;
using R.Entities.UnitOfWorks;
using R.Entities.Services;
using R.Entities.DataContext;
using R.Entities.Entities;
using R.Services.Services;
using R.Repositories;
using Microsoft.EntityFrameworkCore;

namespace R.WebUI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime  
            services.AddDbContext<ReactTemplateContext>(options =>
            {
                options.UseMySql(AppSettings.ConnectionString, ServerVersion.Parse("8.0.22-mysql"));
                options.UseLazyLoadingProxies();
            }
            );

            services.AddScoped<Func<ReactTemplateContext>>((provider) => () => provider.GetService<ReactTemplateContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        //public static IServiceCollection AddRepositories(this IServiceCollection services)
        //{
        //    return services
        //        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        //        .AddScoped<ITopicCategoryRepository, TopicCategoryRepository>()
        //        .AddScoped<ITopicRepository, TopicRepository>()
        //        .AddScoped<IVoteClickRepository, VoteClickRepository>()
        //        .AddScoped<IVoteObjectRepository, VoteObjectRepository>()
        //        .AddScoped<IAnonymousUserRepository, AnonymousUserRepository>();
        //}

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>();
        }
    }
}
