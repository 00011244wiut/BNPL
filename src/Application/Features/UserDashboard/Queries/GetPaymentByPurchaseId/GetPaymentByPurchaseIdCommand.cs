using Application.DTOs.Payment;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetPaymentByPurchaseId;

public record GetPaymentByPurchaseIdCommand(Guid PurchaseId) : IRequest<PaymentResponseDto>;