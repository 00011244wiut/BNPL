using Domain.Common;

namespace Domain.Entities;

public class BankInfoEntity : BaseEntity<Guid>
{
    public Guid MerchantId { get; set; }
    public string MFO { get; set; } = null!;    
    public string BankAccountNumber { get; set; } = null!;
}

// @BankInfoEntity
// - MerchantId: Guid
// - MFO: string
// - BankAccountNumber: string