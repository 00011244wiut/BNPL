using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SchedulesRepository : GenericRepository<SchedulesEntity>, ISchedulesRepository
{
    private readonly ProjectDbContext _context;
    
    public SchedulesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SchedulesEntity?> GetScheduleByPurchaseId(Guid purchaseId)
    {
        return await _context.Schedules.FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
    }
}

// @Schedules