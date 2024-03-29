using FluentValidation;

namespace Application.Features.LegalData.ConfirmLegalData;

// Validator for the ConfirmLegalDataCommand
public class ConfirmLegalDataValidator : AbstractValidator<ConfirmLegalDataCommand>
{
    public ConfirmLegalDataValidator()
    {
        // You can add validation rules here if needed
    }
}