using Application.DTOs.LegalData;
using MediatR;

namespace Application.Features.LegalData.ConfirmLegalData;

public record ConfirmLegalDataCommand(LegalDataRequestDto LegalDataRequestDto, Guid Id) : IRequest<Unit>;