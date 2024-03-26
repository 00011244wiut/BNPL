using Domain.Common;

namespace Domain.Entities;

public class LegalDataEntity : BaseEntity<Guid>
{
    public string City { get; set; } = null!;
    public string BusinessType { get; set; } = null!;
    public string LegalName { get; set; } = null!;
    public string LegalAddress { get; set; } = null!;
    public string DirectorName { get; set; } = null!;
}


// @LegalDataEntity
// - City: string
// - BusinessType: int
// - LegalName: string
// - LegalAddress: string
// - DirectorName: string
