using Microsoft.Extensions.DependencyInjection;

namespace Myafim.FireflyIii.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFireflyIiiClient(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<FireflyIiiClientFactory>();
        return services;
    }
}