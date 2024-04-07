using Application.DTOs.Document;
using Application.DTOs.Merchant;
using MediatR;

namespace Application.Features.Merchant.Commands.UploadDocument;
// UploadDocumentCommand record to handle uploading documents
public record UploadDocumentCommand(UploadDocumentDto UploadDocumentDto, Guid UserId) : IRequest<DocumentResponseDto>;