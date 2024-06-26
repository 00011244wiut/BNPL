using Application.Contracts;  // Importing necessary namespaces
using Application.DTOs.Payment;
using Application.DTOs.Schedule; // Importing necessary namespaces
using Application.Exceptions;  // Importing necessary namespaces
using MediatR;
using IMapper = AutoMapper.IMapper; // Importing necessary namespaces

namespace Application.Features.UserDashboard.Queries.GetPaymentByPurchaseId;  // Namespace declaration

public class GetPaymentByPurchaseIdHandler : IRequestHandler<GetPaymentByPurchaseIdCommand, PaymentResponseDto>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;  // Field declaration
    private readonly IMapper _mapper;
    
    public GetPaymentByPurchaseIdHandler(IUnitOfWork unitOfWork, IMapper mapper)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning constructor parameter to field
        _mapper = mapper;
    }
    
    public async Task<PaymentResponseDto> Handle(GetPaymentByPurchaseIdCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var payment = await _unitOfWork.PaymentsRepository.GetPaymentByPurchaseIdAsync(request.PurchaseId);  // Retrieving payment by purchase ID
        
        if (payment == null) throw new NotFoundException("Payment not found");  // Throwing exception if payment is null
        
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(payment.PurchaseId);  // Retrieving purchase by ID
        if (purchase == null) throw new NotFoundException("Purchase not found");  // Throwing exception if purchase is null
        
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);  // Retrieving product by ID
        if (product == null) throw new NotFoundException("Product not found");  // Throwing exception if product is null

        var schedules = await _unitOfWork.SchedulesRepository.GetScheduleByPurchaseId(request.PurchaseId);

        var schedulesList = schedules!.Select(schedule => _mapper.Map<ScheduleResponseDto>(schedule)).ToList();

        return new PaymentResponseDto(  // Returning payment response DTO
            payment.Id,
            product.ProductName,
            product.CreatedTime,
            product.MerchantId,
            schedulesList
        );
    }
}
