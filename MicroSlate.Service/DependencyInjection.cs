using MicroSlate.Infrastructure.AppConct;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.Abstracts.Auth;
using MicroSlate.Service.Abstracts.Authorization.claims;
using MicroSlate.Service.Abstracts.Authorization.Roles;
using MicroSlate.Service.Abstracts.Email;
using MicroSlate.Service.Helper;
using MicroSlate.Service.Implementions.Auth;
using MicroSlate.Service.Implementions.Authorization.claims;
using MicroSlate.Service.Implementions.Authorization.Roles;
using MicroSlate.Service.Implementions.Email;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroSlate.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailsService, EmailsService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddScoped<IClaimService, claimService>();

            services.AddScoped<IRolesService, RolesService>();


          


            var emailSettings = new EmailSettings();

            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);

            return services;
        }
    }
}
