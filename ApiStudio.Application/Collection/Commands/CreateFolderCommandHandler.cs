using ApiStudio.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Collection.Commands;

public sealed class CreateFolderCommandHandler
    : IRequestHandler<CreateFolderCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateFolderCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateFolderCommand request,
        CancellationToken cancellationToken)
    {
        var collection = await _context.Collections
            .Include(x => x.Folders)
            .FirstOrDefaultAsync(
                x => x.Id == request.CollectionId,
                cancellationToken);

        if (collection is null)
            throw new Exception("Collection not found.");

        if (request.ParentFolderId.HasValue && collection.Folders.All(f => f.Id != request.ParentFolderId))
        {
            throw new Exception("Parent folder not found.");
        }

        var folder = collection.AddFolder(
            request.Name,
            request.ParentFolderId);

        await _context.SaveChangesAsync(cancellationToken);

        return folder.Id;
    }
}