using ApiStudio.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;

public class GetWorkspaceByIdHandler
    : IRequestHandler<GetWorkspaceByIdQuery, WorkspaceDto?>
{
    private readonly IApplicationDbContext _context;

    public GetWorkspaceByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkspaceDto?> Handle(
        GetWorkspaceByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Workspaces
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new WorkspaceDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}