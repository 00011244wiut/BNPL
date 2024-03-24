using Domain.Common;

namespace Domain.Entities;

public class PurchaseEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid CardId { get; set; }
    public Guid ProductId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedTime { get; set; }
}

// @PurchaseEntity
// - UserId: Guid
// - CardId: Guid
// - ProductId: Guid
// - TotalAmount: decimal
// - CreatedTime: DateTime