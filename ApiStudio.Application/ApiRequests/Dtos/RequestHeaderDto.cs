namespace ApiStudio.Application.ApiRequests.Dtos;

public sealed record RequestHeaderDto(
    string Name,
    string Value,
    bool Enabled);