using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class CardsRepository : GenericRepository<CardsEntity>, ICardsRepository
{
    private readonly ProjectDbContext _context;
    
    public CardsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}