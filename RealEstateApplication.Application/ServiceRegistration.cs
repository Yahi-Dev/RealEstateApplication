using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RealEstateApplication.Data
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly()));
        }
    }
}
