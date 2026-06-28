namespace ApiStudio.HttpEngine.Models;

public sealed record HttpQueryParameter(
    string Name,
    string Value,
    bool Enabled = true);