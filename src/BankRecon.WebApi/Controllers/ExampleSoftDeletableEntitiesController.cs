using BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Create;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Delete;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Update;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Queries.GetAll;
using BankRecon.Application.Features.ExampleSoftDeletableEntities.Queries.GetById;
using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.ExampleSoftDeletableEntities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankRecon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleSoftDeletableEntitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExampleSoftDeletableEntitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all entities.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ExampleSoftDeletableEntityDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllExampleSoftDeletableEntitiesQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get entity by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ExampleSoftDeletableEntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetExampleSoftDeletableEntityByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Create a new entity.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ExampleSoftDeletableEntityDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateExampleSoftDeletableEntityCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Result?.Id }, result);
    }

    /// <summary>
    /// Update an existing entity.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ExampleSoftDeletableEntityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateExampleSoftDeletableEntityCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command with { Id = id }, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Soft delete an entity.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteExampleSoftDeletableEntityCommand(id), cancellationToken);
        return NoContent();
    }
}
