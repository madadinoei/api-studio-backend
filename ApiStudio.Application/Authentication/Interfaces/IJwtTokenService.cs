using ApiStudio.Application.Authentication.Dtos;

namespace ApiStudio.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    Task<LoginResponse> GenerateTokenAsync(
        Guid identityUserId,
        CancellationToken cancellationToken);
}