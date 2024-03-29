using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling purchase limit entities.
public interface IPurchaseLimitRepository : IGenericRepository<PurchaseLimitEntity>
{
    // Asynchronously retrieves a purchase limit entity by user ID.
    Task<PurchaseLimitEntity?> GetLimitByUserId(Guid userId);
}