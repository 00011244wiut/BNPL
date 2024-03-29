using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling token entities.
public interface ITokenRepository : IGenericRepository<TokenEntity>
{
    // Asynchronously retrieves a token entity by token value.
    Task<TokenEntity?> GetByTokenAsync(string token);

    // Asynchronously retrieves a token entity by user ID.
    Task<TokenEntity?> GetByUserIdAsync(Guid userId);

    // Asynchronously retrieves a token entity by merchant ID.
    Task<TokenEntity?> GetByMerchantIdAsync(Guid merchantId);
}