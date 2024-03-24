using Domain.Common;

namespace Domain.Entities;

public class UserDocumentsEntity : BaseEntity<Guid>
{
    public int DocumentType { get; set; }
    public string DocumentLink { get; set; } = null!;
}

// @UserDocumentsEntity
// - DocumentType: int
// - DocumentLink: string