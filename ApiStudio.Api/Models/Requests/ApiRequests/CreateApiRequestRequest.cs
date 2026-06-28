using ApiStudio.Application.ApiRequests.Dtos;
using ApiStudio.Domain.Entities;

namespace ApiStudio.Api.Models.Requests.ApiRequests;

public sealed class CreateApiRequestRequest
{
    public string Name { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public HttpMethodType Method { get; set; }
    public List<RequestHeaderDto>? Headers { get; set; }
    public List<QueryParameterDto>? QueryParameters { get; set; }
    public RequestBodyDto? RequestBodyDto { get; set; }
}