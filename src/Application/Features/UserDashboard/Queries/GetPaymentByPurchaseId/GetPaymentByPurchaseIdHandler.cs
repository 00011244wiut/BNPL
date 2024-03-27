using Application.Contracts;
using Application.DTOs.Payment;
using Application.Exceptions;
using MediatR;

namespace Application.Features.UserDashboard.Queries.GetPaymentByPurchaseId;

public class GetPaymentByPurchaseIdHandler : IRequestHandler<GetPaymentByPurchaseIdCommand, PaymentResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPaymentByPurchaseIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PaymentResponseDto> Handle(GetPaymentByPurchaseIdCommand request, CancellationToken cancellationToken)
    {
        var payment = await _unitOfWork.PaymentsRepository.GetPaymentByPurchaseIdAsync(request.PurchaseId);
        
        if (payment == null) throw new NotFoundException("Payment not found");
        
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(payment.PurchaseId);
        if (purchase == null) throw new NotFoundException("Purchase not found");
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);
        if (product == null) throw new NotFoundException("Product not found");
        var schedule =
            await _unitOfWork
                .SchedulesRepository
                .GetByIdAsync(payment.ScheduleId ?? 
                              throw new NotFoundException("Schedule not found"));
        
        return new PaymentResponseDto(
            payment.Id,
            product.ProductName,
            product.CreatedTime,
            product.MerchantId,
            (
                product.PriceAmount,
                schedule!.PaymentDate
            )
            );
    }
}