using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ITokenRepository : IGenericRepository<TokenEntity>
{
    Task<TokenEntity?> GetByTokenAsync(string token);
    Task<TokenEntity?> GetByUserIdAsync(Guid userId);
    Task<TokenEntity?> GetByMerchantIdAsync(Guid merchantId);
}
