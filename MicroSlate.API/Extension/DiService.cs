using MicroSlate.Application;
using MicroSlate.Infrastructure.AppConct;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

namespace MicroSlate.API.Extension
{
    public static class DiService
    {
        public static void AddDiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 🔹 Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //    services.AddAutoMapper(AppDomain.CurrentDomain.Load("4EPlatform.Application"));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly);
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }

        public static void AddLocalizationOptions(this IServiceCollection services)
        {
            #region Localization
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>{
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("ar")};

                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion
        }
    }
}
