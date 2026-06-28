namespace ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;

public sealed class WorkspaceDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string? Description { get; init; }

    public DateTime CreatedAt { get; init; }
}