using Application.DTOs.Purchase;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetPurchaseById;

public record GetPurchaseByIdCommand(Guid PurchaseId) : IRequest<PurchaseResponseDto>;