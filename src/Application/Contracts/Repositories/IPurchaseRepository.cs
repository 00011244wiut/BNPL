using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling purchase entities.
public interface IPurchaseRepository : IGenericRepository<PurchaseEntity>
{
    // Asynchronously retrieves purchase entities by user ID.
    Task<List<PurchaseEntity>?> GetPurchaseByUserId(Guid UserId);
}