// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Repository implementation for PurchaseEntity
public class PurchaseRepository : GenericRepository<PurchaseEntity>, IPurchaseRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize PurchaseRepository with database context
    public PurchaseRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a list of purchase entities by user ID asynchronously
    public async Task<List<PurchaseEntity>?> GetPurchaseByUserId(Guid userId)
    {
        return await _context.Purchases.Where(x => x.UserId == userId).ToListAsync();
    }
}