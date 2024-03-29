using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling product entities.
public interface IProductsRepository : IGenericRepository<ProductsEntity>
{
    
}