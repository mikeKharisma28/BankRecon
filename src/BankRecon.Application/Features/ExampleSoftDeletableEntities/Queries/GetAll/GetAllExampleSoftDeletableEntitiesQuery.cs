using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Queries.GetAll;

public record GetAllExampleSoftDeletableEntitiesQuery : IRequest<ApiResponse<List<ExampleSoftDeletableEntityDto>>>;
