using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class SchedulesEntity : BaseEntity<Guid>
{
    public Guid PurchaseId { get; set; }
    public Guid CardId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public ScheduleState State = ScheduleState.Scheduled;
}

// @SchedulesEntity
// - PurchaseId: Guid
// - CardId: Guid
// - Amount: decimal
// - PaymentDate: DateTime