using Microsoft.Extensions.DependencyInjection;


namespace MicroSlate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return services;
        }
    }
}
