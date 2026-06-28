using ApiStudio.Api.Models.Requests.ApiRequests;
using ApiStudio.Api.Models.Requests.Collections;
using ApiStudio.Api.Models.Requests.Workspaces;
using ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;
using ApiStudio.Application.ApiRequests.Queries;
using ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;
using ApiStudio.Application.Collection.Commands;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/apirequest")]
public class ApiRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{collectionId:guid}/requests")]
    public async Task<IActionResult> CreateRequest(
        Guid collectionId,
        CreateApiRequestRequest request)
    {
        var id = await _mediator.Send(
            new CreateApiRequestCommand(
                collectionId,
                request.Name,
                request.Endpoint,
                request.Method,request.RequestBodyDto,request.Headers,request.QueryParameters));

        return CreatedAtAction(
            nameof(Get),
            new { id },
            id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(
            new GetApiRequestQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}