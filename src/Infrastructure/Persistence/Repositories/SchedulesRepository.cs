// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for SchedulesEntity
public class SchedulesRepository : GenericRepository<SchedulesEntity>, ISchedulesRepository
{
    // Field to store database context
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize SchedulesRepository with database context
    public SchedulesRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }

    // Method to get a schedule entity by purchase ID asynchronously
    public async Task<SchedulesEntity?> GetScheduleByPurchaseId(Guid purchaseId)
    {
        return await _context.Schedules.FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
    }
}