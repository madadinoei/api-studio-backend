using ApiStudio.Domain.Common;
using ApiStudio.Domain.ValueObjects;

namespace ApiStudio.Domain.Entities;

public class ApiRequest : BaseEntity
{
    private readonly List<RequestHeader> _headers = new();

    private readonly List<QueryParameter> _queryParameters = new();

    private ApiRequest()
    {
    }

    public Guid CollectionId { get; private set; }

    public string Name { get; private set; } = default!;

    public HttpMethodType Method { get; private set; }

    public Endpoint Endpoint { get; private set; } = default!;

    public RequestBody Body { get; private set; } = RequestBody.Empty();

    public IReadOnlyCollection<RequestHeader> Headers
        => _headers.AsReadOnly();

    public IReadOnlyCollection<QueryParameter> QueryParameters
        => _queryParameters.AsReadOnly();

    public Collection Collection { get; private set; } = default!;

    public static ApiRequest Create(
        Guid collectionId,
        string name,
        HttpMethodType method,
        Endpoint endpoint)
    {
        return new ApiRequest
        {
            Id = Guid.NewGuid(),
            CollectionId = collectionId,
            Name = name,
            Method = method,
            Endpoint = endpoint,
            Body = RequestBody.Empty(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddHeader(RequestHeader header)
        => _headers.Add(header);

    public void RemoveHeader(string name)
        => _headers.RemoveAll(x => x.Name == name);

    public void AddQueryParameter(QueryParameter parameter)
        => _queryParameters.Add(parameter);

    public void RemoveQueryParameter(string name)
        => _queryParameters.RemoveAll(x => x.Name == name);

    public void SetBody(RequestBody body)
        => Body = body;
}