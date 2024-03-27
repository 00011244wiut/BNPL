using Application.DTOs.Product;
using Application.DTOs.Schedule;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;

public record CreateOrderCommand(Guid ProductId, Guid UserId) : IRequest<(Guid, ProductResponseDto, ScheduleResponseDto)>;