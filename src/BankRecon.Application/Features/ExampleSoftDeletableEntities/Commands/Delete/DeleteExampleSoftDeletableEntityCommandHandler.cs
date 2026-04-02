using BankRecon.Application.Common.Interfaces;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Responses;
using MediatR;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Delete;

public class DeleteExampleSoftDeletableEntityCommandHandler
    : IRequestHandler<DeleteExampleSoftDeletableEntityCommand, ApiResponse>
{
    private readonly IRepository<ExampleSoftDeletableEntity> _repository;

    public DeleteExampleSoftDeletableEntityCommandHandler(IRepository<ExampleSoftDeletableEntity> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse> Handle(
        DeleteExampleSoftDeletableEntityCommand request,
        CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return ApiResponse.Success("Entity deleted successfully.");
    }
}
