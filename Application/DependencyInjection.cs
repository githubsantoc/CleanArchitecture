
using Application.services;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppliServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddSingleton<JWTandRefreshService>();

            services.AddHangfire(conf => conf
                    .UsePostgreSqlStorage(configuration.GetConnectionString("CQRSConnection")));
            services.AddHangfireServer();

            // services.AddAutoMapper(typeof(CustomerMapper));



            return services;
        }
    }
}
