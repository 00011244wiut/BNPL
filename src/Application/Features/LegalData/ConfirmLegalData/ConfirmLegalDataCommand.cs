using Application.DTOs.LegalData;
using MediatR;

namespace Application.Features.LegalData.ConfirmLegalData;

// Command for confirming legal data
public record ConfirmLegalDataCommand(LegalDataRequestDto LegalDataRequestDto, Guid Id) : IRequest<Unit>;