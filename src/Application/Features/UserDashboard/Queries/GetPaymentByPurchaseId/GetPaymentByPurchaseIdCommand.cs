using Application.DTOs.Payment;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetPaymentByPurchaseId;  // Namespace declaration

public record GetPaymentByPurchaseIdCommand(Guid PurchaseId) : IRequest<PaymentResponseDto>;  // Command declaration