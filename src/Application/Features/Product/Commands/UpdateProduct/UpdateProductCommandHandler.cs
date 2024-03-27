using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UpdateProductCommandValidator().ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(request.ProductId);
        var productEntity = _mapper.Map<ProductsEntity>(product);
        _mapper.Map(request.ProductDto, productEntity);
        await _unitOfWork.ProductsRepository.UpdateAsync(productEntity);
        return Unit.Value;
    }
}