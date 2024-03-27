using Domain.Common;

namespace Domain.Entities;

public class ProductsEntity : BaseEntity<Guid>
{
    public Guid BusinessId { get; set; }
    public Guid MerchantId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal PriceAmount { get; set; }
    public string PreviewPhotoLink { get; set; } = null!;
    public DateTime CreatedTime { get; set; }
}

// @ProductsEntity
// - BusinessId: Guid
// - ProductName: string
// - PriceAmount: decimal
// - PreviewPhotoLink: string
// - CreatedTime: DateTime
