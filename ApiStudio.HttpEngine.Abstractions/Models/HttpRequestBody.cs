namespace ApiStudio.HttpEngine.Abstractions.Models;

public sealed record HttpRequestBody(
    HttpBodyType Type,
    string? Content);