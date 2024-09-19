using ClientXP.Application.Services;

namespace ClientXP.Infraestructure.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigServices(this IServiceCollection services)
        {
            services.AddTransient<ClientService>();
            return services;
        }
    }
}
