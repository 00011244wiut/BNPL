using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MerchantRepository : GenericRepository<MerchantEntity>, IMerchantRepository
{
    private readonly ProjectDbContext _context;
    
    public MerchantRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<MerchantEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Merchants.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
    
    public async Task<MerchantEntity?> GetByTaxPayerIdAsync(string taxPayerId)
    {
        return await _context.Merchants.FirstOrDefaultAsync(x => x.TaxPayerId == taxPayerId);
    }
}

// @Merchant