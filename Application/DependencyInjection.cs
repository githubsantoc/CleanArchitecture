
using Application.services;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
