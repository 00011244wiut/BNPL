using Application.DTOs.Auth;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UploadDocument;

public record UploadDocumentCommand(UploadDocumentDto  UploadDocumentDto, Guid UserId) : IRequest<Unit>;