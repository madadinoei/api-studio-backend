namespace ApiStudio.Domain.ValueObjects;

public sealed record RequestHeader
{
    public string Name { get; }

    public string Value { get; }

    public bool Enabled { get; }

    private RequestHeader()
    {
    }

    private RequestHeader(string name, string value, bool enabled)
    {
        Name = name;
        Value = value;
        Enabled = enabled;
    }

    public static RequestHeader Create(
        string name,
        string value,
        bool enabled = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name));

        return new RequestHeader(
            name.Trim(),
            value.Trim(),
            enabled);
    }
}