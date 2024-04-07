using Application.Contracts;
using Application.DTOs.KYC;
using Application.Exceptions;
using AutoMapper;             // Importing necessary namespaces
using Domain.Constants;       // Importing necessary namespaces
using Domain.Entities;        // Importing necessary namespaces
using FluentValidation;       // Importing necessary namespaces
using MediatR;

namespace Application.Features.User.Commands.UploadPhotos;  // Namespace declaration

public class UploadPhotosHandler : IRequestHandler<UploadPhotosCommand, KycResponseDto>  // Class declaration
{
    private readonly IUnitOfWork _unitOfWork;            // Field declaration
    private readonly IMapper _mapper;                    // Field declaration
    private readonly IFileUploadService _fileUploadService;  // Field declaration
    private readonly IFetchApi _fetchApi;  // Field declaration

    public UploadPhotosHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService, IFetchApi fetchApi)  // Constructor declaration
    {
        _unitOfWork = unitOfWork;  // Assigning unit of work
        _mapper = mapper;        // Assigning mapper
        _fileUploadService = fileUploadService;  // Assigning file upload service
        _fetchApi = fetchApi;  // Assigning fetch API
    }
    
    public async Task<KycResponseDto> Handle(UploadPhotosCommand request, CancellationToken cancellationToken)  // Method declaration
    {
        var validationResult = await new UploadPhotosValidator().ValidateAsync(request, cancellationToken);  // Validating upload photos command
        
        if (!validationResult.IsValid)  // Checking if validation fails
        {
            throw new ValidationException(validationResult.Errors);  // Throwing validation exception
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);  // Retrieving user from repository
        
        if (user == null)  // Checking if user is null
            throw new NotFoundException("User not found");  // Throwing not found exception
        
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
        
        var response = await _fetchApi.CallComparePhotosApi(
            request.UploadPicturesDto.Selfie ?? throw new ValidationException("File is required"),
            request.UploadPicturesDto.IdPhoto ?? throw new ValidationException("File is required"));
        
        if (!response.Match)
            throw new ValidationException(
                $"Percentage: {response.Similarity_percentage} Result: Selfie and ID photo do not match,"); // Throwing validation exception if selfie and ID photo do not match
        
        userEntity.UserDocumentId = selfieDocument.Id;  // Assigning user document ID
        
        if (userEntity.UserState == UserState.PhoneNumberConfirmed)  // Checking user state
            userEntity.UserState = UserState.VerificationCompleted;  // Updating user state
        
        if (userEntity.UserState == UserState.VerificationCompleted &&  // Checking user state and properties
            userEntity is { FirstName: not null, PurchaseLimitId: not null, CardId: not null})
            userEntity.UserState = UserState.CompleteProfile;  // Updating user state
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);  // Updating user entity in repository
        return new KycResponseDto(response.Match, response.Similarity_percentage);  // Returning KYC response
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
