using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IPurchaseLimitRepository : IGenericRepository<PurchaseLimitEntity>
{
    Task<PurchaseLimitEntity?> GetLimitByUserId(Guid userId);
}