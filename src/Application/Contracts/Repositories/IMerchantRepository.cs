using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IMerchantRepository : IGenericRepository<MerchantEntity>
{
    Task<MerchantEntity?> GetByPhoneNumberAsync(string phoneNumber);
    Task<MerchantEntity?> GetByTaxPayerIdAsync(string taxPayerId);
}