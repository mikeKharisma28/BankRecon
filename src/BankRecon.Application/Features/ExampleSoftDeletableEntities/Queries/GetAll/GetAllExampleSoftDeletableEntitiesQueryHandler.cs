using AutoMapper;
using BankRecon.Application.Common.Interfaces;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Queries.GetAll;

public class GetAllExampleSoftDeletableEntitiesQueryHandler
    : IRequestHandler<GetAllExampleSoftDeletableEntitiesQuery, ApiResponse<List<ExampleSoftDeletableEntityDto>>>
{
    private readonly IRepository<ExampleSoftDeletableEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllExampleSoftDeletableEntitiesQueryHandler(
        IRepository<ExampleSoftDeletableEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExampleSoftDeletableEntityDto>>> Handle(
        GetAllExampleSoftDeletableEntitiesQuery request,
        CancellationToken cancellationToken)
    {
        List<ExampleSoftDeletableEntity> entities = await _repository.GetAllAsync(cancellationToken);
        List<ExampleSoftDeletableEntityDto> dtos = _mapper.Map<List<ExampleSoftDeletableEntityDto>>(entities);
        return ApiResponse<List<ExampleSoftDeletableEntityDto>>.Success(dtos);
    }
}
