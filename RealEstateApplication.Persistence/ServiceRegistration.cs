using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Persistence.Context;
using RealEstateApplication.Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            if (configuration.GetValue<bool>("UseInMemoryDataBase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)),
                ServiceLifetime.Scoped);
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddTransient<ITypeOfRealEstateRepository, TypeOfRealEstateRepositoy>();
            //services.AddTransient<ITypeOfSaleRepository, TypeOfSaleRepository>();
            services.AddTransient<IImprovementRepository, ImprovementRepository>();
            //services.AddTransient<IRealEstateRepository, RealEstateRepository>();
            //services.AddTransient<IRealEstateImprovementRepository, RealEstateImprovemnetRepository>();
            //services.AddTransient<IRealEstateImageRepository, RealEstateImageRepository>();
            //services.AddTransient<IRealEstateClientRepository, RealEstateClientRepository>();
            #endregion
        }
    }
}
