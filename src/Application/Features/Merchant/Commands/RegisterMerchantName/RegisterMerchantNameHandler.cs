using Application.Contracts;
using Application.DTOs.Auth;
using Application.Exceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterMerchantName;

public class RegisterMerchantNameHandler : IRequestHandler<RegisterMerchantNameCommand, MerchantResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterMerchantNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<MerchantResponseDto> Handle(RegisterMerchantNameCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new RegisterMerchantNameValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.MerchantId);
        
        if (merchant == null)
            throw new NotFoundException("Merchant not found");
        
        var merchantEntity = _mapper.Map<MerchantEntity>(merchant);
        
        merchantEntity.LastName = request.LastName;
        merchantEntity.FirstName = request.FirstName;
        
        // Update merchant status
        if (merchantEntity.MerchantStatus == MerchantStatus.LegalDataObtained &&
            merchantEntity is { BankInfoId: not null, MerchantDocumentsId: not null })
            merchantEntity.MerchantStatus = MerchantStatus.Complete;
        
        await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
        return _mapper.Map<MerchantResponseDto>(merchantEntity);
    }
}