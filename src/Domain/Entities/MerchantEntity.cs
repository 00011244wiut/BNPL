using Domain.Common;

namespace Domain.Entities;

public class MerchantEntity : BaseEntity<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int MerchantStatus { get; set; }
    public string CompanyName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Guid LegalDataId { get; set; }
    public bool IsVerificationSuccess { get; set; }
}

// @MerchantEntity
// - Id: Guid
// - FirstName: string
// - LastName: string
// - MerchantStatus: int
// - CompanyName: string
// - PhoneNumber: string
// - LegalDataId: Guid
// - IsVerificationSuccess: bool
