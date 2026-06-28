using ApiStudio.HttpEngine.Abstractions;
using ApiStudio.HttpEngine.Abstractions.Interfaces;
using ApiStudio.HttpEngine.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace ApiStudio.HttpEngine;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpEngine(
        this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IHttpRequestMessageBuilder, HttpRequestMessageBuilder>();

        services.AddScoped<IHttpRequestExecutor, HttpRequestExecutor>();

        return services;
    }
}