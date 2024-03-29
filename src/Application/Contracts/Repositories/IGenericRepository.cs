namespace Application.Contracts.Repositories;

// Interface for a generic repository providing basic CRUD operations.
// T: The entity type.
public interface IGenericRepository<T> where T : class
{
    // Asynchronously retrieves an entity by its ID.
    Task<T?> GetByIdAsync(Guid id);

    // Asynchronously retrieves all entities.
    Task<IReadOnlyList<T>> GetAllAsync();

    // Asynchronously retrieves a paged response of entities.
    Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);

    // Asynchronously adds an entity to the repository.
    Task<T> AddAsync(T entity);

    // Asynchronously updates an existing entity in the repository.
    Task UpdateAsync(T entity);

    // Asynchronously deletes an entity from the repository.
    Task DeleteAsync(T entity);

    // Asynchronously checks if an entity with the given ID exists in the repository.
    Task<bool> Exists(Guid id);
}