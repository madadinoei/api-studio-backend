using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities;
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
            .FirstOrDefaultAsync(
                x => x.Id == request.CollectionId,
                cancellationToken);

        if (collection is null)
            throw new Exception("Collection not found.");

        if (request.ParentFolderId.HasValue && !_context.Folders.Where(x => x.CollectionId == collection.Id).Any(f => f.Id == request.ParentFolderId))
        {
            throw new Exception("Parent folder not found.");
        }

        //var folder = collection.AddFolder(
        //    request.Name,
        //    request.ParentFolderId);
        var folder = await _context.Folders.AddAsync(Folder.Create(collection.Id, request.Name, request.ParentFolderId), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return folder.Entity.Id;
    }
}