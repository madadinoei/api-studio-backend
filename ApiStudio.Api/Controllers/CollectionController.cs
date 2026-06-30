using ApiStudio.Api.Models.Requests.Collections;
using ApiStudio.Application.Collection.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionController : ControllerBase
{
    private readonly IMediator _mediator;

    public CollectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCollectionRequest request)
    {
        var command = new CreateCollectionCommand(request.WorkspaceId,
            request.Name,
            request.Description);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        //var result = await _mediator.Send(new GetWorkspaceByIdQuery(id));

        //if (result is null)
        //    return NotFound();

        return Ok();
    }
}