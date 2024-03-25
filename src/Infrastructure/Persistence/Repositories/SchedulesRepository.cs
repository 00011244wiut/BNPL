using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class SchedulesRepository : GenericRepository<SchedulesEntity>, ISchedulesRepository
{
    private readonly ProjectDbContext _context;
    
    public SchedulesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @Schedules