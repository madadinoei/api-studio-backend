using ApiStudio.Application.Collection.Dtos;

namespace ApiStudio.Application.Workspaces.Dtos;

public sealed class WorkspaceFullDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string? Description { get; init; }

    public DateTime CreatedAt { get; init; }
    
    public IEnumerable<CollectionDto>? Collections { get; init; }
}