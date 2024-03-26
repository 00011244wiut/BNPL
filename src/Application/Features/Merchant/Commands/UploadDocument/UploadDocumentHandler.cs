using Application.Contracts;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.UploadDocument;

public class UploadDocumentHandler : IRequestHandler<UploadDocumentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileUploadService;
    
    public UploadDocumentHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
    }
    
    public async Task<Unit> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UploadDocumentValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.UserId);
        
        var documentLink = await UploadDocument(request);
        
        var merchantDocument = new MerchantDocumentsEntity()
        {
            BusinessId = new Guid(),
            DocumentType = DocumentTypes.Document,
            DocumentLink = documentLink,
            CreatedTime = DateTime.Now
        };
        
        merchantDocument = await _unitOfWork.MerchantDocumentsRepository.AddAsync(merchantDocument);
        
        merchant!.LegalDataId = merchantDocument.Id;

        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);
        return Unit.Value;
    }
    
    private async Task<string> UploadDocument(UploadDocumentCommand request)
    {
        
        // upload Selfie if exists
        var file = request.UploadDocumentDto.Document;
        
        if (file == null)
        {
            throw new ValidationException("File is required");
        }
        
        var documentString = await _fileUploadService.UploadFileAsync(file);

        return documentString;
    }
}