using MediatR;

namespace Application.Features.Order.Commands.ConfirmOrder;

public record ConfirmOrderCommand(Guid UserId, Guid PurchaseId, Guid CardId) : IRequest<Unit>;