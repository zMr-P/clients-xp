using ClientXP.Infraestructure.Context;
using ClientXP.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientXP.Infraestructure.Config
{
    public static class ContextConfig
    {
        public static IServiceCollection ConfigContext(this IServiceCollection services)
        {
            services.AddDbContext<XpClientsContext>(options =>
            options.UseInMemoryDatabase("xp_dream"));
            services.AddScoped<IDbContext, XpClientsContext>();
            return services;
        }
        public static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<XpClientsContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
