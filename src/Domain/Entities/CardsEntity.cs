using Domain.Common;

namespace Domain.Entities;

public class CardsEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public string CardNumber { get; set; } = null!;
    public string ExpiryDate { get; set; } = null!;
    public int CardType { get; set; }
    public int? CVV { get; set; }
}


// @CardsEntity
// - Id: Guid
// - UserId: Guid
// - CardNumber: string
// - ExpiryDate: string
// - CardType: int
// - CVV: int? (nullable)