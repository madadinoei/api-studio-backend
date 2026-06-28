using ApiStudio.Domain.Enums;

namespace ApiStudio.Application.ApiRequests.Dtos;

public sealed record RequestBodyDto(
    BodyType Type,
    string? Content);