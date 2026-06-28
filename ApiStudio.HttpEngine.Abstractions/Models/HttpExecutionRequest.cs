namespace ApiStudio.HttpEngine.Abstractions.Models;

public sealed class HttpExecutionRequest
{
    public RequestMethod Method { get; init; }

    public string Endpoint { get; init; } = string.Empty;

    public HttpRequestBody Body { get; init; }
        = new(HttpBodyType.None, null);

    public IReadOnlyCollection<HttpHeader> Headers { get; init; }
        = [];

    public IReadOnlyCollection<HttpQueryParameter> QueryParameters { get; init; }
        = [];
}