using ApiStudio.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Collection.Commands;

public class CreateCollectionHandler
    : IRequestHandler<CreateCollectionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCollectionHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateCollectionCommand request,
        CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces
            .AnyAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (!workspace)
            throw new Exception("Workspace not found.");

        var collection = Domain.Entities.Collection.Create(
            request.WorkspaceId,
            request.Name,
            request.Description);

        _context.Collections.Add(collection);

        await _context.SaveChangesAsync(cancellationToken);

        return collection.Id;
    }
}