using Microsoft.AspNetCore.RateLimiting;

namespace YarpApiGateway.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGatewayServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));
            services.AddRateLimiter(opt =>
            {
                opt.AddFixedWindowLimiter("fixed", options =>
                {
                    //Fixed policy only allows 100 Request in 10 seconds
                    options.Window = TimeSpan.FromSeconds(10);
                    options.PermitLimit = 100;
                });
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowLocalhost",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
