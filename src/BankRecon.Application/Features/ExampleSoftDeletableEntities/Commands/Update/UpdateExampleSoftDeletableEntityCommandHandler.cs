using AutoMapper;
using BankRecon.Application.Common.Exceptions;
using BankRecon.Application.Common.Interfaces;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Update;

public class UpdateExampleSoftDeletableEntityCommandHandler
    : IRequestHandler<UpdateExampleSoftDeletableEntityCommand, ApiResponse<ExampleSoftDeletableEntityDto>>
{
    private readonly IRepository<ExampleSoftDeletableEntity> _repository;
    private readonly IMapper _mapper;

    public UpdateExampleSoftDeletableEntityCommandHandler(
        IRepository<ExampleSoftDeletableEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExampleSoftDeletableEntityDto>> Handle(
        UpdateExampleSoftDeletableEntityCommand request,
        CancellationToken cancellationToken)
    {
        ExampleSoftDeletableEntity? entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new EntityNotFoundException(nameof(ExampleSoftDeletableEntity), request.Id);
        }

        entity.Description = request.Description;
        entity.Amount = request.Amount;

        ExampleSoftDeletableEntity updated = await _repository.UpdateAsync(entity, cancellationToken);
        ExampleSoftDeletableEntityDto dto = _mapper.Map<ExampleSoftDeletableEntityDto>(updated);
        return ApiResponse<ExampleSoftDeletableEntityDto>.Success(dto, "Entity updated successfully.");
    }
}
