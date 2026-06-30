using ApiStudio.Api.Models.Requests.ProvisionUser;
using ApiStudio.Application.Authentication.Commands.Login;
using ApiStudio.Application.Authentication.Commands.ProvisionUser;
using ApiStudio.Application.Authentication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApiStudio.Api.Models.Requests.Login;

namespace ApiStudio.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("provision")]
    public async Task<IActionResult> Provision(
        ProvisionUserRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new ProvisionUserCommand(
                request.UserName,
                request.Password),
            cancellationToken);

        return Ok();
    }


    [HttpPost("login")]
    public async Task<OkObjectResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new LoginCommand(
                request.UserName,
                request.Password),
            cancellationToken);

        return Ok(response);
    }
}