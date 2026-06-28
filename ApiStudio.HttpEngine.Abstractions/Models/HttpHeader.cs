namespace ApiStudio.HttpEngine.Abstractions.Models;

public sealed record HttpHeader(
    string Name,
    string Value,
    bool Enabled = true);