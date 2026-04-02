using BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Create;

public record CreateExampleSoftDeletableEntityCommand(
    string Description,
    decimal Amount) : IRequest<ApiResponse<ExampleSoftDeletableEntityDto>>;
