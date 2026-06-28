using ApiStudio.HttpEngine.Abstractions.Models;

namespace ApiStudio.HttpEngine.Abstractions;

public interface IHttpRequestMessageBuilder
{
    HttpRequestMessage Build(HttpExecutionRequest request);
}