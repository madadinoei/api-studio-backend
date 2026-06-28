using ApiStudio.Domain.Common;

namespace ApiStudio.Domain.Entities;

public class Workspace : BaseEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    private readonly List<Collection> _collections = new();

    public IReadOnlyCollection<Collection> Collections => _collections;


    private Workspace()
    {
    }

    public Workspace(string name, string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}