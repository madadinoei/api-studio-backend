namespace ApiStudio.Api.Models.Requests.ProvisionUser;

public sealed class ProvisionUserRequest
{
    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}