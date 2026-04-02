using AutoMapper;
using BankRecon.Application.Common.Interfaces;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Create;

public class CreateExampleSoftDeletableEntityCommandHandler
    : IRequestHandler<CreateExampleSoftDeletableEntityCommand, ApiResponse<ExampleSoftDeletableEntityDto>>
{
    private readonly IRepository<ExampleSoftDeletableEntity> _repository;
    private readonly IMapper _mapper;

    public CreateExampleSoftDeletableEntityCommandHandler(
        IRepository<ExampleSoftDeletableEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExampleSoftDeletableEntityDto>> Handle(
        CreateExampleSoftDeletableEntityCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new ExampleSoftDeletableEntity
        {
            Description = request.Description,
            Amount = request.Amount
        };

        ExampleSoftDeletableEntity created = await _repository.AddAsync(entity, cancellationToken);
        ExampleSoftDeletableEntityDto dto = _mapper.Map<ExampleSoftDeletableEntityDto>(created);
        return ApiResponse<ExampleSoftDeletableEntityDto>.Success(dto, "Entity created successfully.");
    }
}
