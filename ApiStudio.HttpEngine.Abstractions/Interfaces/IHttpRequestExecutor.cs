using ApiStudio.HttpEngine.Abstractions.Models;

namespace ApiStudio.HttpEngine.Abstractions.Interfaces;

public interface IHttpRequestExecutor
{
    Task<HttpExecutionResponse> ExecuteAsync(
        HttpExecutionRequest request,
        CancellationToken cancellationToken = default);
}