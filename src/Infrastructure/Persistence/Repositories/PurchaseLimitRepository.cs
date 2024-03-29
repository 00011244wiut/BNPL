// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;


// Repository implementation for PurchaseLimitEntity
public class PurchaseLimitRepository : GenericRepository<PurchaseLimitEntity>, IPurchaseLimitRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize PurchaseLimitRepository with database context
    public PurchaseLimitRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a purchase limit entity by user ID asynchronously
    public async Task<PurchaseLimitEntity?> GetLimitByUserId(Guid userId)
    {
        return await _context.PurchaseLimits.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}