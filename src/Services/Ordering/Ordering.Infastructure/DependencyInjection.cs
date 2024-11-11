using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ordering.Infastructure.Data.Interceptors;

namespace Ordering.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                var httpContextAccessor = services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
                opt.AddInterceptors(new AuditableEntityInterceptors(httpContextAccessor));
                opt.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}