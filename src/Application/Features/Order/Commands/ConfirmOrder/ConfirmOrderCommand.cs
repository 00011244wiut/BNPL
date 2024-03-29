using MediatR;

namespace Application.Features.Order.Commands.ConfirmOrder;
// ConfirmOrderCommand record to confirm an order
public record ConfirmOrderCommand(Guid UserId, Guid PurchaseId, Guid CardId) : IRequest<Unit>;