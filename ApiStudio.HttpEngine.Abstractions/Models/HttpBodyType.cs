namespace ApiStudio.HttpEngine.Abstractions.Models;

public enum HttpBodyType
{
    None,
    Raw,
    Json,
    Xml,
    FormData,
    UrlEncoded,
    Binary
}