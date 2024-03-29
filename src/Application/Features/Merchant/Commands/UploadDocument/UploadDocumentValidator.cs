using FluentValidation;

namespace Application.Features.Merchant.Commands.UploadDocument;
// UploadDocumentValidator class to validate UploadDocumentCommand
public class UploadDocumentValidator : AbstractValidator<UploadDocumentCommand>
{
    // Constructor for UploadDocumentValidator
    public UploadDocumentValidator()
    {
        // Validation rules for UploadDocumentCommand can be added here if needed
    }
}