using CaseManagement.Application.Cases.Commands.CreateCase;
using Microsoft.Extensions.DependencyInjection;

namespace CaseManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(

            this IServiceCollection services) 
                
            {

            services.AddScoped<CreateCaseCommandHandler>();

            return services;
            }
        }


    }

