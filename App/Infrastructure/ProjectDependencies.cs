using D._Repository.Services;
using E._DAL.SQLServer.Infrastructure;
using F._Application.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace App.Infrastructure
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services,
    IConfiguration configuration)
        {
            var repositoryAssembly = Assembly.GetAssembly(typeof(IAplicationUserManager));

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Command"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Query"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            var serviceAssembly = Assembly.GetAssembly(typeof(JwtUtils));

            services.RegisterAssemblyPublicNonGenericClasses(serviceAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
