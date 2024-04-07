using Application.Contracts;  // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using MediatR;  // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetUserLimit;  // Namespace declaration

public class GetUserLimitHandler : IRequestHandler<GetUserLimitCommand, (string LimitType, decimal MaxAmount)>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    
    public GetUserLimitHandler(IUnitOfWork unitOfWork)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameter to field
    }
    
    public async Task<(string LimitType, decimal MaxAmount)> Handle(GetUserLimitCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        // checking if user exists
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);  // Retrieving user by ID
        if (user == null) throw new NotFoundException("User not found");  // Throwing exception if user is null
        
        var purchaseLimit = await _unitOfWork.PurchaseLimitRepository.GetLimitByUserId(request.UserId);  // Retrieving purchase limit by user ID
        if (purchaseLimit == null) throw new NotFoundException("User Profile not Completed");  // Throwing exception if purchase limit is null
        
        return (purchaseLimit.PurchaseLimitType, purchaseLimit.MaxAmount);  // Returning tuple of limit type and max amount
    }
}