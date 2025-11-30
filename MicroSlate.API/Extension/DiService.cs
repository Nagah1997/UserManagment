using MicroSlate.Application;
using MicroSlate.Infrastructure.AppConct;
using Microsoft.EntityFrameworkCore;
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
    }
}
