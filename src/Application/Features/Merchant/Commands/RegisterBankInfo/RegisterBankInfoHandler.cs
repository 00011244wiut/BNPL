using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

// Handler for the RegisterBankInfoCommand
public record RegisterBankInfoHandler : IRequestHandler<RegisterBankInfoCommand, MerchantResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    // Constructor
    public RegisterBankInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // Handles the command asynchronously
    public async Task<MerchantResponseDto> Handle(RegisterBankInfoCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new RegisterBankInfoValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Get the merchant by ID
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.MerchantId);

        // Create bank information entity
        var bankInfo = new BankInfoEntity()
        {
            MFO = request.RegisterBankInfoDto.MFO,
            BankAccountNumber = request.RegisterBankInfoDto.BankAccountNumber,
            MerchantId = merchant!.Id
        };
        
        // Add bank information to the database
        bankInfo = await _unitOfWork.BankInfoRepository.AddAsync(bankInfo);
        
        // Update merchant with bank info ID
        merchant.BankInfoId = bankInfo.Id;
        
        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);
        
        // Map and return response DTO
        return _mapper.Map<MerchantResponseDto>(merchant);
    }
}