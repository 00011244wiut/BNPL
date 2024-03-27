using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SalesRepository : GenericRepository<SalesEntity>, ISalesRepository
{
    private readonly ProjectDbContext _context;
    
    public SalesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<SalesEntity>?> GetSalesByMerchantId(Guid merchantId)
    {
        return await _context.Sales.Where(x => x.MerchantId == merchantId).ToListAsync();
    }
}

// @Sales