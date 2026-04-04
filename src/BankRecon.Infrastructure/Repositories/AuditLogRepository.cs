using BankRecon.Application.Common.Interfaces;
using BankRecon.Domain.Entities;
using BankRecon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankRecon.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for audit log queries.
/// </summary>
public class AuditLogRepository : IAuditLogRepository
{
    private readonly BankReconDbContext _dbContext;

    public AuditLogRepository(BankReconDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AuditLog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuditLogs
            .OrderByDescending(log => log.Timestamp)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AuditLog>> GetByEntityAsync(
        string entityName,
        string entityId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuditLogs
            .Where(log => log.EntityName == entityName && log.EntityId == entityId)
            .OrderByDescending(log => log.Timestamp)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AuditLog>> GetByEntityNameAsync(
        string entityName,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuditLogs
            .Where(log => log.EntityName == entityName)
            .OrderByDescending(log => log.Timestamp)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AuditLog>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuditLogs
            .Where(log => log.Timestamp >= startDate && log.Timestamp <= endDate)
            .OrderByDescending(log => log.Timestamp)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AuditLog>> GetByActionAsync(
        string action,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.AuditLogs
            .Where(log => log.Action == action)
            .OrderByDescending(log => log.Timestamp)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
