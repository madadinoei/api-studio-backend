namespace ApiStudio.Domain.ValueObjects;

public sealed record QueryParameter
{
    public string Name { get; }

    public string Value { get; }

    public bool Enabled { get; }

    private QueryParameter()
    {
    }

    private QueryParameter(
        string name,
        string value,
        bool enabled)
    {
        Name = name;
        Value = value;
        Enabled = enabled;
    }

    public static QueryParameter Create(
        string name,
        string value,
        bool enabled = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name));

        return new QueryParameter(
            name.Trim(),
            value.Trim(),
            enabled);
    }
}