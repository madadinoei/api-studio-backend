namespace ApiStudio.HttpEngine.Abstractions.Models;

public sealed record HttpQueryParameter(
    string Name,
    string Value,
    bool Enabled = true);