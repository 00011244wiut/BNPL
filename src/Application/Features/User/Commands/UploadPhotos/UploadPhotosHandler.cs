using Application.Contracts;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.UploadPhotos;

public class UploadPhotosHandler : IRequestHandler<UploadPhotosCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileUploadService;
    
    public UploadPhotosHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
    }
    
    public async Task<Unit> Handle(UploadPhotosCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UploadPhotosValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var userEntity = _mapper.Map<UserEntity>(user);
        
        var (selfieLink, idPhotoLink) = await UploadDocument(request);
        
        var selfieDocument = new UserDocumentsEntity()
        {
            DocumentType = DocumentTypes.Selfie,
            DocumentLink = selfieLink
        };
        
        selfieDocument = await _unitOfWork.UserDocumentsRepository.AddAsync(selfieDocument);
        
        var idPhotoDocument = new UserDocumentsEntity()
        {
            DocumentType = DocumentTypes.IdPhoto,
            DocumentLink = idPhotoLink
        };
        
        idPhotoDocument = await _unitOfWork.UserDocumentsRepository.AddAsync(idPhotoDocument);
        
        //@TODO: call KnowYourCustomerService to verify the document
        
        userEntity.UserDocumentId = selfieDocument.Id;
        
        if (userEntity.CardId != null)
            userEntity.UserState = UserState.CompleteProfile;
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return Unit.Value;
    }
    
    private async Task<(string, string)> UploadDocument(UploadPhotosCommand request)
    {
        
        // upload Selfie if exists
        var file = request.UploadPicturesDto.Selfie;
        
        if (file == null)
        {
            throw new ValidationException("File is required");
        }
        
        var SelfieResult = await _fileUploadService.UploadImageAsync(file);
        
        // Upload IdPhoto if exists
        file = request.UploadPicturesDto.IdPhoto;
        if (file == null)
        {
            throw new ValidationException("File is required");
        }
        
        var IdPhotoResult = await _fileUploadService.UploadImageAsync(file);
        
        return (SelfieResult, IdPhotoResult);
    }
}