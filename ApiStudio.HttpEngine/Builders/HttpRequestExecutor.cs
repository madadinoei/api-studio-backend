using System.Diagnostics;
using ApiStudio.HttpEngine.Abstractions;
using ApiStudio.HttpEngine.Abstractions.Interfaces;
using ApiStudio.HttpEngine.Abstractions.Models;

namespace ApiStudio.HttpEngine.Builders;

public sealed class HttpRequestExecutor : IHttpRequestExecutor
{
    private readonly HttpClient _httpClient;
    private readonly IHttpRequestMessageBuilder _builder;

    public HttpRequestExecutor(
        IHttpClientFactory factory,
        IHttpRequestMessageBuilder builder)
    {
        _httpClient = factory.CreateClient();
        _builder = builder;
    }

    public async Task<HttpExecutionResponse> ExecuteAsync(
        HttpExecutionRequest request,
        CancellationToken cancellationToken = default)
    {
        var message = _builder.Build(request);

        var stopwatch = Stopwatch.StartNew();

        var response = await _httpClient.SendAsync(
            message,
            cancellationToken);

        stopwatch.Stop();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        var headers = response.Headers
            .ToDictionary(
                h => h.Key,
                h => h.Value.ToArray());

        foreach (var header in response.Content.Headers)
        {
            headers[header.Key] = header.Value.ToArray();
        }

        return new HttpExecutionResponse
        {
            StatusCode = (int)response.StatusCode,
            ReasonPhrase = response.ReasonPhrase,
            Body = body,
            Duration = stopwatch.Elapsed,
            ContentLength = response.Content.Headers.ContentLength ?? body.Length,
            ContentType = response.Content.Headers.ContentType?.MediaType,
            Headers = headers
        };
    }
}