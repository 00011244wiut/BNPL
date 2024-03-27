using Application.Contracts;
using Application.Exceptions;
using Domain.Constants;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetUserInfo;

public class GetUserInfoHandler : IRequestHandler<GetUserInfoCommand, (string FirstName, string LastName)>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetUserInfoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<(string FirstName, string LastName)> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null) throw new NotFoundException("User not found");
        
        var firstName = user.FirstName ?? throw new BadRequestException("Incomplete user profile");
        var lastName = user.LastName ?? throw new BadRequestException("Incomplete user profile");
        
        return (firstName, lastName);
    }
}