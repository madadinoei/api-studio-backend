namespace ApiStudio.HttpEngine.Abstractions.Models;

public sealed class HttpExecutionResponse
{
    public int StatusCode { get; init; }

    public string? ReasonPhrase { get; init; }

    public string? ContentType { get; init; }

    public string Body { get; init; } = string.Empty;

    public long ContentLength { get; init; }

    public TimeSpan Duration { get; init; }

    public Dictionary<string, string[]> Headers { get; init; }
        = new();
}