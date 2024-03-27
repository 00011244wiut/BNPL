using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PurchaseLimitRepository : GenericRepository<PurchaseLimitEntity>, IPurchaseLimitRepository
{
    private readonly ProjectDbContext _context;
    
    public PurchaseLimitRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PurchaseLimitEntity?> GetLimitByUserId(Guid userId)
    {
        return await _context.PurchaseLimits.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}

// @PurchaseLimit