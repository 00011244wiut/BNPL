// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for CardsEntity
public class CardsRepository : GenericRepository<CardsEntity>, ICardsRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize CardsRepository with database context
    public CardsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}