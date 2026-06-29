using ApiStudio.Application.Authentication.Interfaces;
using MediatR;

namespace ApiStudio.Application.Authentication.Commands.ProvisionUser;

public sealed class ProvisionUserCommandHandler
    : IRequestHandler<ProvisionUserCommand>
{
    private readonly IExternalAuthenticationProvider _authenticationProvider;
    private readonly IUserProvisioningService _userProvisioningService;

    public ProvisionUserCommandHandler(
        IExternalAuthenticationProvider authenticationProvider,
        IUserProvisioningService userProvisioningService)
    {
        _authenticationProvider = authenticationProvider;
        _userProvisioningService = userProvisioningService;
    }

    public async Task Handle(
        ProvisionUserCommand request,
        CancellationToken cancellationToken)
    {
        var authenticatedUser = await _authenticationProvider.AuthenticateAsync(
            request.UserName,
            request.Password,
            cancellationToken);

        if (authenticatedUser is null)
            throw new UnauthorizedAccessException("Invalid username or password.");

        await _userProvisioningService.ProvisionAsync(
            authenticatedUser,
            cancellationToken);
    }
}