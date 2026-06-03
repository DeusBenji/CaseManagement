using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Application.Cases.Queries.GetAllCases;
using CaseManagement.Infrastructure.Peristence;
using CaseManagement.Infrastructure.Peristence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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
            services.AddScoped<GetCasesQueryHandler>();


            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            return services;

        }


    }
}
