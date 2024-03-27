using Application.DTOs.Purchase;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetAllPurchases;

public record GetAllPurchaseCommand(Guid UserId) : IRequest<List<PurchaseResponseDto>>;