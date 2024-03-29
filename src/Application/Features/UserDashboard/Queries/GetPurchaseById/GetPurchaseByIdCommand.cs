using Application.DTOs.Purchase;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetPurchaseById;  // Namespace declaration

public record GetPurchaseByIdCommand(Guid PurchaseId) : IRequest<PurchaseResponseDto>;  // Command declaration