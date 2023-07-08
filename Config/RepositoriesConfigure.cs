using Microsoft.Extensions.DependencyInjection;
using Repositories.Auth;

namespace Config
{
    /// <summary>
    /// Repositories Configure
    /// </summary>
    public static class RepositoriesConfigure
    {
        public static IServiceCollection AddAndConfigureRepositories(this IServiceCollection services)
        {
            // Common
            services.AddTransient<IAuthRepo, AuthRepo>();           

            return services;
        }
    }
}