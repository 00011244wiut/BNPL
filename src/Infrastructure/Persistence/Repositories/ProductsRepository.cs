// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for ProductsEntity
public class ProductsRepository : GenericRepository<ProductsEntity>, IProductsRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize ProductsRepository with database context
    public ProductsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductsEntity>?> GetProductByMerchantId(Guid merchantId)
    {
        return await _context.Products.Where(x => x.MerchantId == merchantId).ToListAsync();
    }
}