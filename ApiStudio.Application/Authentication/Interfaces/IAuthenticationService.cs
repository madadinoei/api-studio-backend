using ApiStudio.Application.Authentication.Dtos;

namespace ApiStudio.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(
        string userName,
        string password,
        CancellationToken cancellationToken);
}