using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling merchant entities.
public interface IMerchantRepository : IGenericRepository<MerchantEntity>
{
    // Asynchronously retrieves a merchant entity by phone number.
    Task<MerchantEntity?> GetByPhoneNumberAsync(string phoneNumber);

    // Asynchronously retrieves a merchant entity by tax payer ID.
    Task<MerchantEntity?> GetByTaxPayerIdAsync(string taxPayerId);
}