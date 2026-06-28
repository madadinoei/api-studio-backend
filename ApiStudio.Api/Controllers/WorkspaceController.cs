using ApiStudio.Api.Models.Requests.Workspaces;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkspaceRequest request)
    {
        var command = new CreateWorkspaceCommand(
            request.Name,
            request.Description);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetWorkspaceByIdQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}