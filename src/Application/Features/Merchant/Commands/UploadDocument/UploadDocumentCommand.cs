using Application.DTOs.Merchant;
using MediatR;

namespace Application.Features.Merchant.Commands.UploadDocument;

public record UploadDocumentCommand(UploadDocumentDto UploadDocumentDto, Guid UserId) : IRequest<Unit>;