using Domain.Entities;

namespace Application.Contracts.Repositories;

// Interface for a repository handling schedules entities.
public interface ISchedulesRepository : IGenericRepository<SchedulesEntity>
{
    // Asynchronously retrieves a schedule entity by purchase ID.
    Task<SchedulesEntity?> GetScheduleByPurchaseId(Guid purchaseId);
}