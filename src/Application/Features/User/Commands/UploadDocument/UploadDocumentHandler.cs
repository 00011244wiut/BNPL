using Application.Contracts;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.UploadDocument;

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

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var userEntity = _mapper.Map<UserEntity>(user);

        var userDocument = new UserDocumentsEntity()
        {
            DocumentType = request.UploadDocumentDto.DocumentType,
            DocumentLink = await UploadDocument(request)
        };
        
        userDocument = await _unitOfWork.UserDocumentsRepository.AddAsync(userDocument);
        
        //@TODO: call KnowYourCustomerService to verify the document
        
        userEntity.UserDocumentId = userDocument.Id;
        userEntity.UserState = userEntity.UserState != UserState.CompleteProfile
            ? UserState.VerificationCompleted
            : UserState.CompleteProfile;
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return Unit.Value;
    }
    
    private async Task<string> UploadDocument(UploadDocumentCommand request)
    {
        // upload file if exists
        var file = request.UploadDocumentDto.PictureFile;
        
        if (file == null)
        {
            throw new ValidationException("File is required");
        }
        
        var uploadResult = await _fileUploadService.UploadImageAsync(file);
        return uploadResult;
    }
}