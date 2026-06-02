using CaseManagement.Application.Cases.Commands.CreateCase;
using Microsoft.Extensions.DependencyInjection;
using CaseManagement.Application.Cases.Queries.GetCaseById;

namespace CaseManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
                
            {

            services.AddScoped<CreateCaseCommandHandler>();
            services.AddScoped<GetCaseByIdQueryHandler>();

            return services;
            }
        }


    }

