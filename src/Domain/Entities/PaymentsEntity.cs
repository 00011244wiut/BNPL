using Domain.Common;

namespace Domain.Entities;

public class PaymentsEntity : BaseEntity<Guid>
{
    public Guid PurchaseId { get; set; }
    public Guid? ScheduleId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}


// @PaymentsEntity
// - PurchaseId: Guid
// - ScheduleId: Guid? (nullable)
// - PaymentDate: DateTime
// - Amount: decimal