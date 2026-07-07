using ApiStudio.Api.Models.Requests.ApiRequests;
using ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;
using ApiStudio.Application.ApiRequests.Commands.SendApiRequest;
using ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;
using ApiStudio.HttpEngine.Abstractions.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/requests")]
public class ApiRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest(CreateApiRequestRequest request)
    {
        var id = await _mediator.Send(
            new CreateApiRequestCommand(
                request.CollectionId,request.FolderId,
                request.Name,
                request.Endpoint,
                request.Method,request.Body,request.Headers,request.QueryParameters));

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