using BankRecon.Domain.Common;

namespace BankRecon.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task HardDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task RestoreAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
}
