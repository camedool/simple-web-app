using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

/// <summary>
/// Generic interface for all repositories.
/// For brevity skipped notion of Unit of Work.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
    where T : class, IDbItem, new()
{
    Task<T?> GetAsync(long id, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<T>> GetAsync(
       CancellationToken cancellationToken = default);

    Task<T> AddAsync(T item, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
