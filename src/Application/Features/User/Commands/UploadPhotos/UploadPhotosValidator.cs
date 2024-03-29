using FluentValidation;  // Importing necessary namespaces

namespace Application.Features.User.Commands.UploadPhotos;  // Namespace declaration

public class UploadPhotosValidator : AbstractValidator<UploadPhotosCommand>  // Class declaration
{
    public UploadPhotosValidator()  // Constructor declaration
    {
        
    }
}