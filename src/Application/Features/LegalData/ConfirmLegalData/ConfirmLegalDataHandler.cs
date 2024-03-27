using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.LegalData.ConfirmLegalData;

public record ConfirmLegalDataHandler : IRequestHandler<ConfirmLegalDataCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ConfirmLegalDataHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ConfirmLegalDataCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new ConfirmLegalDataValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.Id);
        var merchantEntity = _mapper.Map<MerchantEntity>(merchant);
        
        if (merchantEntity.MerchantStatus == MerchantStatus.PhoneNumberConfirmed)
            merchantEntity.MerchantStatus = MerchantStatus.LegalDataObtained;
        
        LegalDataEntity? legalData;
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
            
            merchantEntity.LegalDataId = legalData!.Id;
            await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
            
            return Unit.Value;
        }
        var legalId = merchant.LegalDataId ?? throw new NotFoundException("LegalDataId does not exist");
        legalData = await _unitOfWork.LegalDataRepository.GetByIdAsync(legalId);
        var legalDataEntity = _mapper.Map(request.LegalDataRequestDto, legalData);
        await _unitOfWork.LegalDataRepository.UpdateAsync(legalDataEntity!);
        
        merchantEntity.LegalDataId = legalData!.Id;
        await _unitOfWork.MerchantRepository.UpdateAsync(merchantEntity);
        
        return Unit.Value;
    }
}