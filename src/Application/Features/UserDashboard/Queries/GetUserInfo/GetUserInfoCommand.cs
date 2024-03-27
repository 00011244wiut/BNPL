using MediatR;

namespace Application.Features.UserDashboard.Queries.GetUserInfo;

public record GetUserInfoCommand(Guid UserId) : IRequest<(string FirstName, string LastName)>;