using Application.Contracts;
using Application.DTOs.Auth;
using Application.DTOs.LegalData;
using Application.Features.User.Commands.RegisterUserInfo;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;

public class RegisterMerchantInfoHandler : IRequestHandler<RegisterMerchantInfoCommand, LegalDataResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterMerchantInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<LegalDataResponseDto> Handle(RegisterMerchantInfoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new RegisterMerchantInfoValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.MerchantId);
        var merchantEntity = _mapper.Map<MerchantEntity>(merchant);

        merchantEntity.CompanyName = request.RegisterMerchantInfoDto.CompanyName;
        merchantEntity.TaxPayerId = request.RegisterMerchantInfoDto.TaxPayerId;
        
        
        var mockLegalData = await _unitOfWork.LegalDataRepository.MockLegalData(request.RegisterMerchantInfoDto.City);
        
        merchantEntity.TaxPayerId = request.RegisterMerchantInfoDto.TaxPayerId;
        merchantEntity.CompanyName = request.RegisterMerchantInfoDto.CompanyName;
        await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
        return _mapper.Map<LegalDataResponseDto>(mockLegalData);
    }
}