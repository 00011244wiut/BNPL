using Application.Contracts;
using Application.DTOs.Purchase;
using Application.Exceptions;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetAllPurchases;

public class GetAllPurchaseHandler : IRequestHandler<GetAllPurchaseCommand, List<PurchaseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllPurchaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<PurchaseResponseDto>> Handle(GetAllPurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetAllAsync();
        
        var purchaseResponse = new List<PurchaseResponseDto>();
        
        foreach (var purchase in purchases)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);
            if (product == null) throw new NotFoundException("Product not found");
            purchaseResponse.Add(new PurchaseResponseDto(
                purchase.Id,
                product.ProductName,
                purchase.CreatedTime
                ));
        }
        
        return purchaseResponse;
    }
}