using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Queries.GetById;

public record GetExampleSoftDeletableEntityByIdQuery(Guid Id) : IRequest<ApiResponse<ExampleSoftDeletableEntityDto>>;
