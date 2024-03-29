using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.LegalData.ConfirmLegalData;

// Handler for the ConfirmLegalDataCommand
public record ConfirmLegalDataHandler : IRequestHandler<ConfirmLegalDataCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // Constructor
    public ConfirmLegalDataHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Handles the command asynchronously
    public async Task<Unit> Handle(ConfirmLegalDataCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new ConfirmLegalDataValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Get the merchant by ID
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.Id);
        var merchantEntity = _mapper.Map<MerchantEntity>(merchant);
        
        // Update merchant status if necessary
        if (merchantEntity.MerchantStatus == MerchantStatus.PhoneNumberConfirmed)
            merchantEntity.MerchantStatus = MerchantStatus.LegalDataObtained;
        
        LegalDataEntity? legalData;

        // If merchant's legal data ID is null, create a new legal data entity
        if (merchant!.LegalDataId == null)
        {
            legalData = new LegalDataEntity()
            {
                BusinessType = request.LegalDataRequestDto.BusinessType,
                City = request.LegalDataRequestDto.City,
                LegalName = request.LegalDataRequestDto.LegalName,
                LegalAddress = request.LegalDataRequestDto.LegalAddress,
                DirectorName = request.LegalDataRequestDto.DirectorName
            };
            legalData = await _unitOfWork.LegalDataRepository.AddAsync(legalData);
            
            merchantEntity.LegalDataId = legalData.Id;
            await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
            
            return Unit.Value;
        }

        // Otherwise, update existing legal data entity
        var legalId = merchant.LegalDataId ?? throw new NotFoundException("LegalDataId does not exist");
        legalData = await _unitOfWork.LegalDataRepository.GetByIdAsync(legalId);
        var legalDataEntity = _mapper.Map(request.LegalDataRequestDto, legalData);
        await _unitOfWork.LegalDataRepository.UpdateAsync(legalDataEntity!);
        
        merchantEntity.LegalDataId = legalData!.Id;
        await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
        
        return Unit.Value;
    }
}
