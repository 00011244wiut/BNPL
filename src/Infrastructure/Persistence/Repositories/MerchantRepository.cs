// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Repository implementation for MerchantEntity
public class MerchantRepository : GenericRepository<MerchantEntity>, IMerchantRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize MerchantRepository with database context
    public MerchantRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
    
    // Method to get a merchant entity by phone number asynchronously
    public async Task<MerchantEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Merchants.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
    
    // Method to get a merchant entity by taxpayer ID asynchronously
    public async Task<MerchantEntity?> GetByTaxPayerIdAsync(string taxPayerId)
    {
        return await _context.Merchants.FirstOrDefaultAsync(x => x.TaxPayerId == taxPayerId);
    }
}