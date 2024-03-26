using Domain.Common;

namespace Domain.Entities;

public class TokenEntity : BaseEntity<Guid>
{
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);

    public Guid? UserId { get; set; }
    public Guid? MerchantId { get; set; }
    public UserEntity User { get; set; } = null!;
    public MerchantEntity Merchant { get; set; } = null!;
}

// @TokenEntity
// - Id: Guid
// - RefreshToken: string
// - Expires: DateTime
// - UserId: Guid
// - User: UserEntity
