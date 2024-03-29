using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetUserLimit;  // Namespace declaration

public record GetUserLimitCommand(Guid UserId) : IRequest<(string LimitType, decimal MaxAmount)>;  // Command declaration