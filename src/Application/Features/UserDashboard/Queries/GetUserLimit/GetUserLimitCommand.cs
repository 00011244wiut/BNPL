using MediatR;

namespace Application.Features.UserDashboard.Queries.GetUserLimit;

public record GetUserLimitCommand(Guid UserId) : IRequest<(string LimitType, decimal MaxAmount)>;