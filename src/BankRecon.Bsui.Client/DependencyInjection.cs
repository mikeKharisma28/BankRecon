using BankRecon.Bsui.Client.Common.Interfaces;
using BankRecon.Bsui.Client.Common.Services;
using BankRecon.Bsui.Client.Features.AuditLogs;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecon.Bsui.Client;

/// <summary>
/// Dependency injection registration for the Bsui.Client layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all Bsui.Client services including the base API client
    /// and feature-specific services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="baseAddress">The base address of the WebApi.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddBsuiClient(
        this IServiceCollection services,
        string baseAddress)
    {
        // Register base API client with typed HttpClient
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });

        // Register feature services
        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;
    }
}
