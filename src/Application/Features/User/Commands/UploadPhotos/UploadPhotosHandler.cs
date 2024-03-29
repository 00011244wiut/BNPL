using Application.Contracts;  // Importing necessary namespaces
using AutoMapper;             // Importing necessary namespaces
using Domain.Constants;       // Importing necessary namespaces
using Domain.Entities;        // Importing necessary namespaces
using FluentValidation;       // Importing necessary namespaces
using MediatR;                // Importing necessary namespaces

namespace Application.Features.User.Commands.UploadPhotos;  // Namespace declaration

public class UploadPhotosHandler : IRequestHandler<UploadPhotosCommand, Unit>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;            // Field declaration
    private readonly IMapper _mapper;                    // Field declaration
    private readonly IFileUploadService _fileUploadService;  // Field declaration
    
    public UploadPhotosHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;           // Assigning constructor parameters to fields
        _mapper = mapper;                   // Assigning constructor parameters to fields
        _fileUploadService = fileUploadService;  // Assigning constructor parameters to fields
    }
    
    public async Task<Unit> Handle(UploadPhotosCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var validationResult = await new UploadPhotosValidator().ValidateAsync(request, cancellationToken);  // Validating upload photos command
        
        if (!validationResult.IsValid)  // Checking if validation fails
        {
            throw new ValidationException(validationResult.Errors);  // Throwing validation exception
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);  // Retrieving user from repository
        var userEntity = _mapper.Map<UserEntity>(user);  // Mapping user to user entity
        
        var (selfieLink, idPhotoLink) = await UploadDocument(request);  // Uploading documents
        
        var selfieDocument = new UserDocumentsEntity()  // Creating selfie document entity
        {
            DocumentType = DocumentTypes.Selfie,   // Assigning document type
            DocumentLink = selfieLink              // Assigning document link
        };
        
        selfieDocument = await _unitOfWork.UserDocumentsRepository.AddAsync(selfieDocument);  // Adding selfie document to repository
        
        var idPhotoDocument = new UserDocumentsEntity()  // Creating ID photo document entity
        {
            DocumentType = DocumentTypes.IdPhoto,   // Assigning document type
            DocumentLink = idPhotoLink              // Assigning document link
        };
        
        await _unitOfWork.UserDocumentsRepository.AddAsync(idPhotoDocument);  // Adding ID photo document to repository
        
        //@TODO: call KnowYourCustomerService to verify the document
        
        userEntity.UserDocumentId = selfieDocument.Id;  // Assigning user document ID
        
        if (userEntity.UserState == UserState.PhoneNumberConfirmed)  // Checking user state
            userEntity.UserState = UserState.VerificationCompleted;  // Updating user state
        
        if (userEntity.UserState == UserState.VerificationCompleted &&  // Checking user state and properties
            userEntity is { FirstName: not null, PurchaseLimitId: not null, CardId: not null})
            userEntity.UserState = UserState.CompleteProfile;  // Updating user state
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);  // Updating user entity in repository
        return Unit.Value;  // Returning unit
    }
    
    private async Task<(string, string)> UploadDocument(UploadPhotosCommand request)  // Method declaration
    {
        
        // upload Selfie if exists  // Comment indicating upload process
        var file = request.UploadPicturesDto.Selfie;  // Retrieving selfie from upload pictures DTO
        
        if (file == null)  // Checking if selfie file is null
        {
            throw new ValidationException("File is required");  // Throwing validation exception if file is null
        }
        
        var SelfieResult = await _fileUploadService.UploadImageAsync(file);  // Uploading selfie image
        
        // Upload IdPhoto if exists  // Comment indicating upload process
        file = request.UploadPicturesDto.IdPhoto;  // Retrieving ID photo from upload pictures DTO
        if (file == null)  // Checking if ID photo file is null
        {
            throw new ValidationException("File is required");  // Throwing validation exception if file is null
        }
        
        var IdPhotoResult = await _fileUploadService.UploadImageAsync(file);  // Uploading ID photo image
        
        return (SelfieResult, IdPhotoResult);  // Returning uploaded document links
    }
}
