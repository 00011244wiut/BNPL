using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class MerchantEntity : BaseEntity<Guid>
{
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public MerchantStatus MerchantStatus { get; set; }
    public string? CompanyName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? TaxPayerId { get; set; } = null!;
    public bool IsVerificationSuccess { get; set; } = false;
    public Guid? LegalDataId { get; set; }
    public Guid? BankInfoId { get; set; }
    public Guid? MerchantDocumentsId { get; set; }
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
