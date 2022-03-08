using HomeWallet.Access.Repositories;
using HomeWallet.Application;
using HomeWallet.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace University.Web.Extensions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddHomeWalletServises(this IServiceCollection services)
        {
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<OperationService, OperationService>();
            services.AddScoped<BalanceService, BalanceService>();
            services.AddScoped<CategoryService, CategoryService>();

            return services;
        }
    }
}