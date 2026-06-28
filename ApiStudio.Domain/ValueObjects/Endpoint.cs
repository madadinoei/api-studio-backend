using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Domain.ValueObjects;
[Owned]
public sealed class Endpoint
{
    private Endpoint()
    {
        // برای EF Core
    }

    private Endpoint(string value)
    {
        Value = value;
    }

    public string Value { get; private set; } = default!;

    public static Endpoint Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Endpoint cannot be empty.", nameof(value));

        return new Endpoint(value.Trim());
    }

    public override string ToString() => Value;
}