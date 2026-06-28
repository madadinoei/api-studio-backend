namespace ApiStudio.Application.ApiRequests.Dtos;

public sealed record QueryParameterDto(
    string Name,
    string Value,
    bool Enabled);