using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class UserEntity : BaseEntity<Guid>

{
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public UserState UserState { get; set; }
    public Guid? PurchaseLimitId { get; set; }
    public bool? IsPhoneConfirmed { get; set; }
    public bool? IsPhoneVerificationSuccess { get; set; }
    public Guid? UserDocumentId { get; set; }
    public Guid? CardId { get; set; }
}

// @UserEntity
// - Id: Guid
// - FirstName: string
// - LastName: string
// - PhoneNumber: string
// - UserState: int
// - PurchaseLimitId: Guid
// - IsPhoneConfirmed: bool? (nullable)
// - IsPhoneVerificationSuccess: bool? (nullable)
