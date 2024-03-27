using Application.Contracts;
using Application.Exceptions;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetUserLimit;

public class GetUserLimitHandler : IRequestHandler<GetUserLimitCommand, (string LimitType, decimal MaxAmount)>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetUserLimitHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<(string LimitType, decimal MaxAmount)> Handle(GetUserLimitCommand request, CancellationToken cancellationToken)
    {
        var purchaseLimit = await _unitOfWork.PurchaseLimitRepository.GetLimitByUserId(request.UserId);
        if (purchaseLimit == null) throw new NotFoundException("User Profile not Completed");
        
        return (purchaseLimit.PurchaseLimitType, purchaseLimit.MaxAmount);
    }
}