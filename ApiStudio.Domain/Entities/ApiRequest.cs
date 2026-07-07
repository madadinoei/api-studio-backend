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
    public Guid? FolderId { get; private set; }

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
        Guid collectionId,Guid? folderId,
        string name,
        string method,
        Endpoint endpoint)
    {
        Enum.TryParse(method, out HttpMethodType methodType);

        return new ApiRequest
        {
            Id = Guid.NewGuid(),
            CollectionId = collectionId,
            FolderId = folderId,
            Name = name,
            Method = methodType,
            Endpoint = endpoint,
            Body = RequestBody.Empty(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddHeader(RequestHeader header)
    {
        _headers.Add(header);
    }

    public void RemoveHeader(string name)
    {
        _headers.RemoveAll(x =>
            x.Key.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void ReplaceHeaders(IEnumerable<RequestHeader> headers)
    {
        _headers.Clear();
        _headers.AddRange(headers);
    }

    public void AddQueryParameter(QueryParameter parameter)
        => _queryParameters.Add(parameter);

    public void RemoveQueryParameter(string name)
        => _queryParameters.RemoveAll(x => x.Name == name);

    public void ReplaceQueryParameters(IEnumerable<QueryParameter> parameters)
    {
        _queryParameters.Clear();
        _queryParameters.AddRange(parameters);
    }

    public void SetBody(RequestBody body)
        => Body = body;
}