using ApiStudio.Application.Authentication.Dtos;
using ApiStudio.Application.Authentication.Interfaces;
using MediatR;

namespace ApiStudio.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        return _authenticationService.LoginAsync(
            request.UserName,
            request.Password,
            cancellationToken);
    }
}