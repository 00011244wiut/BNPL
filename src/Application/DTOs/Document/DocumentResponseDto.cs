using Domain.Constants;

namespace Application.DTOs.Document;

public record DocumentResponseDto(Guid Id, DocumentTypes DocumentType, string DocumentLink, DateTime CreatedTime);