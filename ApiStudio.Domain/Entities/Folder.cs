using ApiStudio.Domain.Common;

namespace ApiStudio.Domain.Entities;

public sealed class Folder : BaseEntity
{
    private Folder()
    {
    }

    private Folder(
        Guid collectionId,
        string name,
        Guid? parentFolderId)
    {
        Id = Guid.NewGuid();
        CollectionId = collectionId;
        Name = name;
        ParentFolderId = parentFolderId;
    }

    public Guid CollectionId { get; private set; }

    public Guid? ParentFolderId { get; private set; }

    public string Name { get; private set; }

    public static Folder Create(
        Guid collectionId,
        string name,
        Guid? parentFolderId)
    {
        return new(
            collectionId,
            name,
            parentFolderId);
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void Move(Guid? parentFolderId)
    {
        ParentFolderId = parentFolderId;
    }
}