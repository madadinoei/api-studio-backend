namespace ApiStudio.HttpEngine.Models;

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