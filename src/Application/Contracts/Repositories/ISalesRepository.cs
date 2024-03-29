using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling sales entities.
public interface ISalesRepository : IGenericRepository<SalesEntity>
{
    // Asynchronously retrieves sales entities by merchant ID.
    Task<List<SalesEntity>?> GetSalesByMerchantId(Guid merchantId);
}