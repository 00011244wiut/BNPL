using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;

public class UserDocumentsEntity : BaseEntity<Guid>
{
    public DocumentTypes DocumentType { get; set; }
    public string DocumentLink { get; set; } = null!;
}

// @UserDocumentsEntity
// - DocumentType: int
// - DocumentLink: string