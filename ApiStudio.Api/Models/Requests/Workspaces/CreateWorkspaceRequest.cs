namespace ApiStudio.Api.Models.Requests.Workspaces;

public class CreateWorkspaceRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}