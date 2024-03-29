using Application.DTOs.Purchase;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetAllPurchases;  // Namespace declaration

public record GetAllPurchaseCommand(Guid UserId) : IRequest<List<PurchaseResponseDto>>;  // Command declaration