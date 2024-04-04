
using Application.common.Authentication;
using Application.Mapper;
using Application.services;
using Application.Wrapper;
using Domains.repository;
using Infrastructure.Authentication;
using Infrastructure.context;
using Infrastructure.ModelDto;
using Infrastructure.persistance;
using Infrastructure.persistance.validator;
using Infrastructure.persistance.WrapperImp;
using Infrastructure.services;
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

            services.AddTransient<IMailServices, MailServices>();
            services.AddTransient<IJwtToken, JwtTokenGenerator>();

            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IEmailExistsValidator, EmailExistsValidation>();

            return services;
        }
    }
}
