using BankRecon.Bsui.Client.Common;
using BankRecon.Bsui.Client.Common.Interfaces;
using BankRecon.Bsui.Client.Common.Options;
using BankRecon.Bsui.Client.Common.Services;
using BankRecon.Bsui.Client.Features;
using BankRecon.Bsui.Client.Features.Interfaces;
using BankRecon.Bsui.Client.Features.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecon.Bsui.Client;

/// <summary>
/// Dependency injection registration for the Bsui.Client layer.
/// Registers all Bsui.Client services including HTTP client configuration,
/// API client implementation, and feature-specific services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all Bsui.Client services including the base API client
    /// and feature-specific services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddBsuiClient(this IServiceCollection services, IConfiguration configuration)
    {
        // Register common services and options
        services.AddBsuiClientCommon(configuration);

        // Register feature-specific services
        services.AddBsuiClientFeatures();

        return services;
    }
}
