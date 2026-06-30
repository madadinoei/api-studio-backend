using MediatR;

namespace ApiStudio.Application.Collection.Commands
{
    public sealed record CreateFolderCommand(
        Guid CollectionId,
        Guid? ParentFolderId,
        string Name)
        : IRequest<Guid>;
}