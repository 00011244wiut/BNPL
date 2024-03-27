using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IPurchaseRepository : IGenericRepository<PurchaseEntity>
{
    Task<List<PurchaseEntity>?> GetPurchaseByUserId(Guid UserId);
}