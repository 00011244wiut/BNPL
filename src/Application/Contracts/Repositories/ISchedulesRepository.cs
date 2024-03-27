using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISchedulesRepository : IGenericRepository<SchedulesEntity>
{
    Task<SchedulesEntity?> GetScheduleByPurchaseId(Guid purchaseId);
}