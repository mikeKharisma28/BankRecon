using BankRecon.Application.Common.Interfaces;
using BankRecon.Domain.Common;
using BankRecon.Domain.Common.Interfaces;
using BankRecon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankRecon.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly BankReconDbContext _dbContext;

    public Repository(BankReconDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTimeOffset.UtcNow;
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return;
        }

        // If entity implements ISoftDeletable, perform soft delete
        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = true;
            softDeletable.DeletedAt = DateTimeOffset.UtcNow;
            _dbContext.Set<T>().Update(entity);
        }
        else
        {
            // Otherwise, perform hard delete
            _dbContext.Set<T>().Remove(entity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Permanently deletes an entity from the database, bypassing soft delete.
    /// Use with caution - only for entities that support hard deletion.
    /// </summary>
    public async Task HardDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<T>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is not null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Restores a soft-deleted entity.
    /// </summary>
    public async Task RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<T>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = false;
            softDeletable.DeletedAt = null;
            softDeletable.DeletedBy = null;
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Gets all entities including soft-deleted ones.
    /// </summary>
    public async Task<List<T>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>()
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
