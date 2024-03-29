// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Generic repository implementation
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    // Field to store database context
    private readonly ProjectDbContext _context;

    // Constructor to initialize GenericRepository with database context
    public GenericRepository(ProjectDbContext context)
    {
        _context = context;
    }

    // Method to get paged response asynchronously
    public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
    {
        return await _context.Set<T>().Skip((page - 1) * size).Take(size).ToListAsync();
    }

    // Method to get entity by Id asynchronously
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    // Method to get all entities asynchronously
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    // Method to add entity asynchronously
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    // Method to update entity asynchronously
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // Method to delete entity asynchronously
    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    // Method to check if entity exists asynchronously
    public async Task<bool> Exists(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        return item != null;
    }
}
