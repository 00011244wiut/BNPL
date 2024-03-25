using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class PurchaseLimitRepository : GenericRepository<PurchaseLimitEntity>, IPurchaseLimitRepository
{
    private readonly ProjectDbContext _context;
    
    public PurchaseLimitRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @PurchaseLimit