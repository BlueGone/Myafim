using Microsoft.Extensions.DependencyInjection;
using Myafim.Domain.Repositories;
using Myafim.Infrastructure.Repositories;

namespace Myafim.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();

        return services;
    }
}