using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.ConfirmOrder;

public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ConfirmOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new ConfirmOrderValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(request.PurchaseId);
        
        if (purchase == null)
        {
            throw new NotFoundException("Purchase not found");
        }
        
        if (purchase.UserId != request.UserId || purchase.CardId != request.CardId)
        {
            throw new BadRequestException("Invalid purchase");
        }
        
        var schedule = await _unitOfWork.SchedulesRepository.GetScheduleByPurchaseId(request.PurchaseId);

        var payment = new PaymentsEntity()
        {
            Amount = purchase.TotalAmount,
            PurchaseId = purchase.Id,
            ScheduleId = (schedule ?? throw new NotFoundException("Schedule not found")).Id,
            PaymentDate = schedule.PaymentDate
        };
        
        await _unitOfWork.PaymentsRepository.AddAsync(payment);

        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(purchase.ProductId);

        var sales = new SalesEntity()
        {
            CreatedTime = DateTime.UtcNow,
            MerchantId = (product ?? throw new NotFoundException("Product not found")).MerchantId,
            ProductId = purchase.ProductId
        };
        
        await _unitOfWork.SalesRepository.AddAsync(sales);

        return Unit.Value;
    }
}