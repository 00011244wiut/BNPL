using Domain.Common;

namespace Domain.Entities;

public class PurchaseLimitEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public DateTime ScoringDate { get; set; }
    public string PurchaseLimitType { get; set; } = null!;
    public decimal MaxAmount { get; set; }
}

// @PurchaseLimitEntity
// - UserId: Guid
// - ScoringDate: DateTime
// - PurchaseLimitType: string
// - MaxAmount: decimal