using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities;
using ApiStudio.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;

public sealed class CreateApiRequestCommandHandler
    : IRequestHandler<CreateApiRequestCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateApiRequestCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateApiRequestCommand request,
        CancellationToken cancellationToken)
    {
        var collection = await _context.Collections
            .FirstOrDefaultAsync(x => x.Id == request.CollectionId, cancellationToken);

        if (collection is null)
            throw new KeyNotFoundException("Collection not found.");

        var apiRequest = ApiRequest.Create(
            request.CollectionId,request.FolderId,
            request.Name,
            request.Method,
            Endpoint.Create(request.Endpoint));
        foreach (var header in request.Headers)
        {
            apiRequest.AddHeader(
                RequestHeader.Create(
                    header.Name,
                    header.Value,
                    header.Enabled));
        }
        foreach (var parameter in request.QueryParameters)
        {
            apiRequest.AddQueryParameter(
                QueryParameter.Create(
                    parameter.Name,
                    parameter.Value,
                    parameter.Enabled));
        }
        _context.ApiRequests.Add(apiRequest);

        await _context.SaveChangesAsync(cancellationToken);

        return apiRequest.Id;
    }
}