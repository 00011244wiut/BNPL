using Application.DTOs.User; // Importing user related DTOs
using MediatR; // Importing MediatR for handling commands and queries

namespace Application.Features.User.Commands.UploadPhotos; // Namespace declaration for the UploadPhotosCommand feature

// Defining a record representing the UploadPhotosCommand
public record UploadPhotosCommand(UploadPicturesDto  UploadPicturesDto, Guid UserId) : IRequest<Unit>;
// 'UploadPhotosCommand' is a request to upload photos, containing an UploadPicturesDto and the UserId,
// and it does not return any response (Unit)