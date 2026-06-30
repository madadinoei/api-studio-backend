using ApiStudio.Domain.Common;

namespace ApiStudio.Domain.Entities;

public class Collection : BaseEntity
{
    public Guid WorkspaceId { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public Workspace Workspace { get; private set; } = default!;

    private Collection()
    {
    }

    private Collection(Guid workspaceId, string name, string? description)
    {
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public static Collection Create(Guid workspaceId, string name, string? description)
    {
        return new Collection(workspaceId,name,description);
    }
}