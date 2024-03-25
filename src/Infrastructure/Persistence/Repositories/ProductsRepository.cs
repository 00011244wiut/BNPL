using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ProductsRepository : GenericRepository<ProductsEntity>, IProductsRepository
{
    private readonly ProjectDbContext _context;
    
    public ProductsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Products