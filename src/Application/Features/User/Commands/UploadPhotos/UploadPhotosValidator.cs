using FluentValidation;

namespace Application.Features.User.Commands.UploadPhotos;

public class UploadPhotosValidator : AbstractValidator<UploadPhotosCommand>
{
    public UploadPhotosValidator()
    {
        
    }
}