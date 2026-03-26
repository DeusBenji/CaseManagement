using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Infrastructure.Peristence;
using CaseManagement.Infrastructure.Peristence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CaseManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services
            ,IConfiguration configuration)
        {
            var connectioString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectioString));

            services.AddScoped<ICaseRepository, CaseRepository>();

            return services;

        }


    }
}
