using Application.Contracts;  // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetUserInfo;  // Namespace declaration

public class GetUserInfoHandler : IRequestHandler<GetUserInfoCommand, (string FirstName, string LastName)>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    
    public GetUserInfoHandler(IUnitOfWork unitOfWork)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameter to field
    }
    
    public async Task<(string FirstName, string LastName)> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);  // Retrieving user by ID
        if (user == null) throw new NotFoundException("User not found");  // Throwing exception if user is null
        
        var firstName = user.FirstName ?? throw new BadRequestException("Incomplete user profile");  // Retrieving first name or throwing exception if null
        var lastName = user.LastName ?? throw new BadRequestException("Incomplete user profile");  // Retrieving last name or throwing exception if null
        
        return (firstName, lastName);  // Returning tuple of first and last name
    }
}