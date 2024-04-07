using Application.Contracts;
using Application.DTOs.Document;
using Application.Exceptions;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Merchant.Commands.UploadDocument;
// UploadDocumentHandler class to handle uploading documents
public class UploadDocumentHandler : IRequestHandler<UploadDocumentCommand, DocumentResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUploadService _fileUploadService;
    
    // Constructor for UploadDocumentHandler
    public UploadDocumentHandler(IUnitOfWork unitOfWork, IFileUploadService fileUploadService)
    {
        _unitOfWork = unitOfWork;
        _fileUploadService = fileUploadService;
    }
    
    // Method to handle uploading documents
    public async Task<DocumentResponseDto> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        // Validate the request using UploadDocumentValidator
        var validationResult = await new UploadDocumentValidator().ValidateAsync(request, cancellationToken);
        
        // If validation fails, throw ValidationException
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Retrieve merchant by user ID
        var merchant = await _unitOfWork.MerchantRepository.GetByIdAsync(request.UserId);
        
        // If merchant is null, throw NotFoundException
        if (merchant == null)
            throw new NotFoundException("Merchant not found");
        
        // Upload document and get the document link
        var documentLink = await UploadDocument(request);
        
        // Create a new merchant document entity
        var merchantDocument = new MerchantDocumentsEntity()
        {
            BusinessId = new Guid(),
            DocumentType = DocumentTypes.Document,
            DocumentLink = documentLink,
            CreatedTime = DateTime.UtcNow
        };
        
        // Add the merchant document to the repository
        merchantDocument = await _unitOfWork.MerchantDocumentsRepository.AddAsync(merchantDocument);
        
        // Update the legal data ID of the merchant
        merchant!.MerchantDocumentsId = merchantDocument.Id;
        
        // Update merchant status
        if (merchant.MerchantStatus == MerchantStatus.LegalDataObtained &&
            merchant is { FirstName: not null, BankInfoId: not null })
            merchant.MerchantStatus = MerchantStatus.Complete;

        // Update the merchant entity in the repository
        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);
        return new DocumentResponseDto(merchantDocument.Id, merchantDocument.DocumentType,
            merchantDocument.DocumentLink, merchantDocument.CreatedTime);
    }
    
    // Method to upload document asynchronously
    private async Task<string> UploadDocument(UploadDocumentCommand request)
    {
        // Upload document if exists
        var file = request.UploadDocumentDto.Document;
        
        // Throw ValidationException if file is null
        if (file == null)
        {
            throw new ValidationException("File is required");
        }
        
        // Upload the file using the file upload service
        var documentString = await _fileUploadService.UploadFileAsync(file);

        return documentString;
    }
}
