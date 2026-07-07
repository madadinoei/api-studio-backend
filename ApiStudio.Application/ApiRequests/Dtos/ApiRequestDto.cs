using ApiStudio.Domain.Entities;

namespace ApiStudio.Application.ApiRequests.Dtos;

public sealed class ApiRequestDto
{
    public Guid Id { get; init; }

    public Guid CollectionId { get; init; }

    public string Name { get; init; } = default!;

    public string Method { get; init; }

    public string Endpoint { get; init; } = default!;

    public RequestBodyDto Body { get; init; } = default!;

    public List<RequestHeaderDto> Headers { get; init; } = [];

    public List<QueryParameterDto> QueryParameters { get; init; } = [];
}