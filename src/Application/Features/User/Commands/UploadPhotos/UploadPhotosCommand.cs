using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UploadPhotos;

public record UploadPhotosCommand(UploadPicturesDto  UploadPicturesDto, Guid UserId) : IRequest<Unit>;