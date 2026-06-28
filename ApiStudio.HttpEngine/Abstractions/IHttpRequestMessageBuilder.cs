using ApiStudio.HttpEngine.Models;

namespace ApiStudio.HttpEngine.Abstractions;

public interface IHttpRequestMessageBuilder
{
    HttpRequestMessage Build(HttpExecutionRequest request);
}