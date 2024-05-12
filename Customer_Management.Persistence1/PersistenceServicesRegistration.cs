using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer_Management.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
            IConfiguration configuration) 
        {
            services.AddDbContext<DbContext>(option => 
            {
                option.UseSqlServer(configuration.GetConnectionString("CustomerManagementConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICustomerRepository,CustomerRepository>();

            
            return services;

        }
    } 
}
