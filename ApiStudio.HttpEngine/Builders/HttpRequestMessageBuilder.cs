using ApiStudio.HttpEngine.Abstractions;
using ApiStudio.HttpEngine.Models;

namespace ApiStudio.HttpEngine.Builders;

public sealed class HttpRequestMessageBuilder
    : IHttpRequestMessageBuilder
{
    public HttpRequestMessage Build(HttpExecutionRequest request)
    {
        var uriBuilder = new UriBuilder(request.Endpoint);

        if (request.QueryParameters.Any())
        {
            var query = string.Join("&",
                request.QueryParameters
                    .Where(x => x.Enabled)
                    .Select(x =>
                        $"{Uri.EscapeDataString(x.Name)}={Uri.EscapeDataString(x.Value)}"));

            uriBuilder.Query = query;
        }

        var message = new HttpRequestMessage(
            ConvertMethod(request.Method),
            uriBuilder.Uri);

        foreach (var header in request.Headers.Where(x => x.Enabled))
        {
            message.Headers.TryAddWithoutValidation(
                header.Name,
                header.Value);
        }

        if (request.Body.Type != HttpBodyType.None &&
            request.Body.Content is not null)
        {
            message.Content = new StringContent(request.Body.Content);

            if (request.Body.Type == HttpBodyType.Json)
            {
                message.Content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }
        }

        return message;
    }

    private static HttpMethod ConvertMethod(RequestMethod method)
        => method switch
        {
            RequestMethod.Get => HttpMethod.Get,
            RequestMethod.Post => HttpMethod.Post,
            RequestMethod.Put => HttpMethod.Put,
            RequestMethod.Delete => HttpMethod.Delete,
            RequestMethod.Patch => HttpMethod.Patch,
            RequestMethod.Head => HttpMethod.Head,
            RequestMethod.Options => HttpMethod.Options,
            _ => throw new NotSupportedException()
        };
}