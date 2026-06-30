namespace ApiStudio.Api.Models.Requests.Collections;

public sealed class CreateFolderRequest
{
    public Guid? ParentFolderId { get; init; }

    public required string Name { get; init; }
}