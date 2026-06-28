namespace ApiStudio.HttpEngine.Models;

public sealed record HttpRequestBody(
    HttpBodyType Type,
    string? Content);