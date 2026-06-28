using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities;
using ApiStudio.Domain.Enums;
using ApiStudio.HttpEngine.Abstractions;
using ApiStudio.HttpEngine.Abstractions.Interfaces;
using ApiStudio.HttpEngine.Abstractions.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.ApiRequests.Commands.SendApiRequest;

public sealed class SendApiRequestCommandHandler
    : IRequestHandler<SendApiRequestCommand, HttpExecutionResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpRequestExecutor _executor;

    public SendApiRequestCommandHandler(
        IApplicationDbContext context,
        IHttpRequestExecutor executor)
    {
        _context = context;
        _executor = executor;
    }

    public async Task<HttpExecutionResponse> Handle(
        SendApiRequestCommand request,
        CancellationToken cancellationToken)
    {
        var apiRequest = await _context.ApiRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.RequestId,
                cancellationToken);

        if (apiRequest is null)
            throw new KeyNotFoundException("Request not found.");

        var executionRequest = Map(apiRequest);

        return await _executor.ExecuteAsync(
            executionRequest,
            cancellationToken);
    }

    private static HttpExecutionRequest Map(ApiRequest request)
    {
        return new HttpExecutionRequest
        {
            Method = request.Method switch
            {
                HttpMethodType.Get => RequestMethod.Get,
                HttpMethodType.Post => RequestMethod.Post,
                HttpMethodType.Put => RequestMethod.Put,
                HttpMethodType.Delete => RequestMethod.Delete,
                HttpMethodType.Patch => RequestMethod.Patch,
                HttpMethodType.Head => RequestMethod.Head,
                HttpMethodType.Options => RequestMethod.Options,
                _ => throw new NotSupportedException()
            },

            Endpoint = request.Endpoint.Value,

            Body = new HttpRequestBody(MapBodyType(request.Body.Type),
                request.Body.Content),

            Headers = request.Headers
                .Select(x => new HttpHeader(
                    x.Name,
                    x.Value,
                    x.Enabled))
                .ToList(),

            QueryParameters = request.QueryParameters
                .Select(x => new HttpQueryParameter(
                    x.Name,
                    x.Value,
                    x.Enabled))
                .ToList()
        };
    }
    private static HttpBodyType MapBodyType(BodyType type)
    {
        return type switch
        {
            BodyType.None => HttpBodyType.None,
            BodyType.Raw => HttpBodyType.Raw,
            BodyType.Json => HttpBodyType.Json,
            BodyType.Xml => HttpBodyType.Xml,
            BodyType.FormData => HttpBodyType.FormData,
            BodyType.UrlEncoded => HttpBodyType.UrlEncoded,
            BodyType.Binary => HttpBodyType.Binary,
            _ => throw new NotSupportedException()
        };
    }
}