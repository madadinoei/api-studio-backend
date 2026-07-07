namespace ApiStudio.Application.ApiRequests.Dtos;

public sealed record RequestHeaderDto(
    string Key,
    string Value,
    bool Enabled);