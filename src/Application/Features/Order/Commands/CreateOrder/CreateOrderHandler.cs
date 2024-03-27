using Application.Contracts;
using Application.DTOs.Product;
using Application.DTOs.Schedule;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, (ProductResponseDto, ScheduleResponseDto)>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<(ProductResponseDto, ScheduleResponseDto)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new CreateOrderValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.ProductId);
        
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        var purchase = new PurchaseEntity()
        {
            CardId = user!.CardId ?? throw new NotFoundException("User did not complete their profile"),
            ProductId = product.Id,
            UserId = user.Id,
            TotalAmount = product.PriceAmount,
            CreatedTime = DateTime.UtcNow

        };
        
        purchase = await _unitOfWork.PurchaseRepository.AddAsync(purchase);

        var schedule = new SchedulesEntity()
        {
            CardId = user.CardId ?? throw new NotFoundException("User did not complete their profile"),
            PurchaseId = purchase.Id,
            Amount = purchase.TotalAmount,
            PaymentDate = DateTime.UtcNow.AddDays(1),
        };
        
        schedule = await _unitOfWork.SchedulesRepository.AddAsync(schedule);
        
        return (_mapper.Map<ProductResponseDto>(product), _mapper.Map<ScheduleResponseDto>(schedule));
    }
}