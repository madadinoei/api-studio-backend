using ApiStudio.Application.ApiRequests.Dtos;
using ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;
using ApiStudio.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;

public class GetApiRequestByIdHandler
    : IRequestHandler<GetApiRequestQuery, ApiRequestDto?>
{
    private readonly IApplicationDbContext _context;

    public GetApiRequestByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiRequestDto?> Handle(
        GetApiRequestQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ApiRequests
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ApiRequestDto
            {
                Id = x.Id,
                CollectionId = x.CollectionId,
                Name = x.Name,
                Method = x.Method,
                Endpoint = x.Endpoint.Value,

                Body = new RequestBodyDto(
                    x.Body.Type,
                    x.Body.Content),

                Headers = x.Headers
                    .Select(h => new RequestHeaderDto(
                        h.Key,
                        h.Value,
                        h.Enabled))
                    .ToList(),

                QueryParameters = x.QueryParameters
                    .Select(q => new QueryParameterDto(
                        q.Name,
                        q.Value,
                        q.Enabled))
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}