using BankRecon.Bsui.Client.Features.Interfaces;
using BankRecon.Bsui.Client.Features.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecon.Bsui.Client.Features;
public static class DependencyInjection
{
    public static IServiceCollection AddBsuiClientFeatures(this IServiceCollection services)
    {
        // Register feature-specific services here
        // Example: services.AddScoped<IFeatureService, FeatureService>();

        // Register feature services
        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;
    }
}
