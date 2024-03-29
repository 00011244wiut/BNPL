// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for SalesEntity
public class SalesRepository : GenericRepository<SalesEntity>, ISalesRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize SalesRepository with database context
    public SalesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
    
    // Method to get a list of sales entities by merchant ID asynchronously
    public async Task<List<SalesEntity>?> GetSalesByMerchantId(Guid merchantId)
    {
        return await _context.Sales.Where(x => x.MerchantId == merchantId).ToListAsync();
    }
}