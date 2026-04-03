using BankRecon.Shared.Features.ExampleSoftDeletableEntities.Dtos;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Update;

public record UpdateExampleSoftDeletableEntityCommand(
    Guid Id,
    string Description,
    decimal Amount) : IRequest<ApiResponse<ExampleSoftDeletableEntityDto>>;
