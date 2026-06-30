using ApiStudio.Application.Authentication.Dtos;
using ApiStudio.Application.Authentication.Interfaces;

namespace ApiStudio.Infrastructure.Authentication.Services;

public sealed class AuthenticationService
    : IAuthenticationService
{
    private readonly IExternalAuthenticationProvider _provider;
    private readonly IUserProvisioningService _provisioningService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationService(
        IExternalAuthenticationProvider provider,
        IUserProvisioningService provisioningService,
        IJwtTokenService jwtTokenService)
    {
        _provider = provider;
        _provisioningService = provisioningService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> LoginAsync(
        string userName,
        string password,
        CancellationToken cancellationToken)
    {
        var externalUser =
            await _provider.AuthenticateAsync(
                userName,
                password,
                cancellationToken);

        if (externalUser is null)
            throw new UnauthorizedAccessException();

        var identityUserId =
            await _provisioningService.ProvisionAsync(
                externalUser,
                cancellationToken);

        return await _jwtTokenService.GenerateTokenAsync(
            identityUserId,
            cancellationToken);
    }
}