using ApiStudio.Api.Models.Requests.Workspaces;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.Application.Workspaces.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWorkspaceQueryService _workspaceQueryService;

    public WorkspaceController(IMediator mediator, IWorkspaceQueryService workspaceQueryService)
    {
        _mediator = mediator;
        _workspaceQueryService = workspaceQueryService;
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


    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var result = await _workspaceQueryService.GetWorkspaceListQuery();

        if (result is null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = _workspaceQueryService.GetByIdAsync(id);

        if (result is null)
            return NoContent();

        return Ok(result);
    }
    [HttpGet("{id:guid}/collections")]
    public async Task<IActionResult> GetDetailById(Guid id)
    {
        var collectionDtos = await _workspaceQueryService.GetWorkspaceCollectionsAsync(id);

        if (collectionDtos is null)
            return NotFound();

        return Ok(collectionDtos);
    }
}