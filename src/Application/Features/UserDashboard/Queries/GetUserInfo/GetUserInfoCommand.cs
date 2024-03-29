using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetUserInfo;  // Namespace declaration

public record GetUserInfoCommand(Guid UserId) : IRequest<(string FirstName, string LastName)>;  // Command declaration