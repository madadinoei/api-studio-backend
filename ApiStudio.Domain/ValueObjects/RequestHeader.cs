namespace ApiStudio.Domain.ValueObjects;

public sealed record RequestHeader
{
    public string Key { get; }

    public string Value { get; }

    public bool Enabled { get; }

    private RequestHeader()
    {
    }

    private RequestHeader(string key, string value, bool enabled)
    {
        Key = key;
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