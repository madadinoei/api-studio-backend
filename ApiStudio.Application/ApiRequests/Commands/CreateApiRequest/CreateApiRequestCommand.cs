using ApiStudio.Application.ApiRequests.Dtos;
using ApiStudio.Domain.Entities;
using MediatR;

namespace ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;

public sealed record CreateApiRequestCommand(
    Guid CollectionId,
    Guid? FolderId,
    string Name,
    string Endpoint,
    HttpMethodType Method,
    RequestBodyDto? Body,
    List<RequestHeaderDto>? Headers,
    List<QueryParameterDto>? QueryParameters
) : IRequest<Guid>;