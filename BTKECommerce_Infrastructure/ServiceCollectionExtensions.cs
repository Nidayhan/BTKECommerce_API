using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Extensions.Token;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.Repository;
using BTKECommerce_Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(BaseResponseModel<>));
            services.AddScoped<ITokenService, TokenService>();
            // Add infrastructure services here
            return services;
        }
    }
}
