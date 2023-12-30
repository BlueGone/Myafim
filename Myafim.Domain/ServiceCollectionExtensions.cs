using Microsoft.Extensions.DependencyInjection;
using Myafim.Domain.Handlers;

namespace Myafim.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ListAccountsHandler>();
        services.AddScoped<GetAccountBalanceHandler>();

        services.AddScoped<ListCategoriesHandler>();

        services.AddScoped<ListTransactionsHandler>();

        services.AddScoped<ImportFireflyIiiIHandler>();

        return services;
    }
}