namespace ApiStudio.Application.Authentication.Dtos;

public sealed class LoginResponse
{
    public required string AccessToken { get; init; }

    public required DateTime ExpiresAt { get; init; }

    public string? RefreshToken { get; init; }
}