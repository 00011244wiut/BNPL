using Application.Contracts;
using Application.DTOs.Purchase;
using Application.Exceptions;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetPurchaseById;

public class GetPurchaseByIdHandler : IRequestHandler<GetPurchaseByIdCommand, PurchaseResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPurchaseByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PurchaseResponseDto> Handle(GetPurchaseByIdCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(request.PurchaseId);
        
        if (purchase == null) throw new NotFoundException("Purchase not found");
        
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);
        if (product == null) throw new NotFoundException("Product not found");
        
        return new PurchaseResponseDto(
            purchase.Id,
            product.ProductName,
            purchase.CreatedTime
            );
    }
}