using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blood.Contract.Repositories.Interface;
using Blood.Repositories.UOW;

namespace Blood.Services
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
