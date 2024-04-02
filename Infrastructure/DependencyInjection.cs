
using Application.Mapper;
using Application.Wrapper;
using Domains.repository;
using Infrastructure.context;
using Infrastructure.repository;
using Infrastructure.WrapperImp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services,  IConfiguration Configuration)
        {
            services.AddDbContext<CQRSDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("CQRSConnection"));
                   
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<string>>()
                .AddRoles<IdentityRole<string>>()
                .AddEntityFrameworkStores<CQRSDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<ApplicationUser>>();

            services.AddAutoMapper(typeof(UserMapper));

            services.AddScoped<IUserWrapper, UserManagerWrapper>();
            services.AddScoped<IRoleWrapper, RoleManagerWrapper>();

            services.AddTransient<ICustomerRepo, CustomerRepo>();

            return services;
        }
    }
}
