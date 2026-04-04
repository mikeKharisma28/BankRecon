using BankRecon.Application.Common.Interfaces;
using BankRecon.Infrastructure.Data;
using BankRecon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<BankReconDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(
                    typeof(BankReconDbContext).Assembly.GetName().Name)));

        // Register generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Register audit log repository
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        return services;
    }
}
