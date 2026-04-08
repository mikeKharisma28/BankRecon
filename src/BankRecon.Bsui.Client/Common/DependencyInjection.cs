using BankRecon.Bsui.Client.Common.Interfaces;
using BankRecon.Bsui.Client.Common.Options;
using BankRecon.Bsui.Client.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecon.Bsui.Client.Common;
public static class DependencyInjection
{
    public static IServiceCollection AddBsuiClientCommon(this IServiceCollection services, IConfiguration configuration)
    {
        // Register common services here
        // Example: services.AddScoped<ICommonService, CommonService>();

        // Register common options
        services.Configure<BackEndOptions>(configuration.GetSection(BackEndOptions.SectionKey));

        // Register HttpClient factory
        services.AddHttpClient<IApiClient, ApiClient>();

        // Register API client
        services.AddScoped<IApiClient, ApiClient>();

        return services;
    }
}
