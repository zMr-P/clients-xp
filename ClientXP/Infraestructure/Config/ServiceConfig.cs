using ClientXP.Application.Services;
using ClientXP.Application.Services.Interfaces;
using ClientXP.Domain.Interfaces;
using ClientXP.Infraestructure.Repositories;

namespace ClientXP.Infraestructure.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigServices(this IServiceCollection services)
        {
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            return services;
        }
    }
}
