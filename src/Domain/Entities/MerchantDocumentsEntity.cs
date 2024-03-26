using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class MerchantDocumentsEntity : BaseEntity<Guid>
{
    public Guid BusinessId { get; set; }
    public DocumentTypes DocumentType { get; set; }
    public string DocumentLink { get; set; } = null!;
    public DateTime CreatedTime { get; set; }
}


// @MerchantDocumentsEntity
// - BusinessId: Guid
// - DocumentType: int
// - DocumentLink: string
// - CreatedTime: DateTime