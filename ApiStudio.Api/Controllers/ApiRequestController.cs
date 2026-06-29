using ApiStudio.Api.Models.Requests.ApiRequests;
using ApiStudio.Api.Models.Requests.Collections;
using ApiStudio.Api.Models.Requests.Workspaces;
using ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;
using ApiStudio.Application.ApiRequests.Commands.SendApiRequest;
using ApiStudio.Application.ApiRequests.Queries;
using ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;
using ApiStudio.Application.Collection.Commands;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;
using ApiStudio.HttpEngine.Abstractions.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/apirequests")]
public class ApiRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{collectionId:guid}")]
    public async Task<IActionResult> CreateRequest(
        Guid collectionId,
        CreateApiRequestRequest request)
    {
        Console.WriteLine(HttpContext);
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


    [HttpPost("{id:guid}/send")]
    [ProducesResponseType(typeof(HttpExecutionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HttpExecutionResponse>> Send(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new SendApiRequestCommand(id),
            cancellationToken);

        return Ok(response);
    }
}