using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

public record RegisterBankInfoHandler : IRequestHandler<RegisterBankInfoCommand, MerchantResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterBankInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<MerchantResponseDto> Handle(RegisterBankInfoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new RegisterBankInfoValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.MerchantId);

        var bankInfo = new BankInfoEntity()
        {
            MFO = request.RegisterBankInfoDto.MFO,
            BankAccountNumber = request.RegisterBankInfoDto.BankAccountNumber,
            MerchantId = merchant!.Id
        };
        
        bankInfo = await _unitOfWork.BankInfoRepository.AddAsync(bankInfo);
        
        merchant!.BankInfoId = bankInfo.Id;
        
        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);
        return _mapper.Map<MerchantResponseDto>(merchant);
    }
}