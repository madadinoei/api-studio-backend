using Microsoft.Extensions.DependencyInjection;

namespace ApiStudio.Http;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpEngine(
        this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IHttpRequestExecutor, HttpRequestExecutor>();

        services.AddScoped<IHttpRequestMessageBuilder, HttpRequestMessageBuilder>();

        return services;
    }
}
