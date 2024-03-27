using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PurchaseRepository : GenericRepository<PurchaseEntity>, IPurchaseRepository
{
    private readonly ProjectDbContext _context;
    
    public PurchaseRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<PurchaseEntity>?> GetPurchaseByUserId(Guid UserId)
    {
        return await _context.Purchases.Where(x => x.UserId == UserId).ToListAsync();
    }
}

// @Purchase