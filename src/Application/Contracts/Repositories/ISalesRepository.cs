using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISalesRepository : IGenericRepository<SalesEntity>
{
    Task<List<SalesEntity>?> GetSalesByMerchantId(Guid merchantId);
}