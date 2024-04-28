using Application.DTOs.Product;
using Application.DTOs.Schedule;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;
// CreateOrderCommand record to create an order
public record CreateOrderCommand(Guid ProductId, Guid UserId) : IRequest<(bool, Guid, ProductResponseDto, List<ScheduleResponseDto>)>;