using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class MerchantRepository : GenericRepository<MerchantEntity>, IMerchantRepository
{
    private readonly ProjectDbContext _context;
    
    public MerchantRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Merchant