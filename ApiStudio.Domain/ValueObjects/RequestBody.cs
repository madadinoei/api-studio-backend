using ApiStudio.Domain.Enums;

namespace ApiStudio.Domain.ValueObjects;

public sealed record RequestBody
{
    public BodyType Type { get; }

    public string? Content { get; }

    private RequestBody()
    {
    }

    private RequestBody(
        BodyType type,
        string? content)
    {
        Type = type;
        Content = content;
    }

    public static RequestBody Empty()
        => new(BodyType.None, null);

    public static RequestBody Create(
        BodyType type,
        string? content)
    {
        return new(type, content);
    }
}