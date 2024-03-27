using Domain.Common;

namespace Domain.Entities;

public class SalesEntity : BaseEntity<Guid>
{
    public Guid MerchantId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreatedTime { get; set; }
}

// @SalesEntity
// - BusinessId: Guid
// - ProductId: Guid
// - CreatedTime: DateTime
