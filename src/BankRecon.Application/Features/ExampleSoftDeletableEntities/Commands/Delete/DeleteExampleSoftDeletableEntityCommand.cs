using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Delete;

public record DeleteExampleSoftDeletableEntityCommand(Guid Id) : IRequest<ApiResponse>;
