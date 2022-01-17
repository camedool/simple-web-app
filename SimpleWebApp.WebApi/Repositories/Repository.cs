using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public class Repository<T> : IRepository<T>
    where T : class, IDbItem, new()
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public virtual async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
    {
        item.Created = DateTime.UtcNow;
        item.Modified = DateTime.UtcNow;

        await _dbSet.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        item.Modified = DateTime.UtcNow;

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        return item;
    }

    public virtual async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        T item = new() { Id = id };

        _dbSet.Remove(item);
        var numberOfSaved = await _context.SaveChangesAsync(cancellationToken);
        return numberOfSaved == 1;
    }

    public virtual async Task<T?> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        var items = await _dbSet.AsNoTracking()
            .ToListAsync(cancellationToken);

        return items.AsReadOnly();
    }
}
