using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class PurchaseRepository : GenericRepository<PurchaseEntity>, IPurchaseRepository
{
    private readonly ProjectDbContext _context;
    
    public PurchaseRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Purchase