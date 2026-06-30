using ApiStudio.Domain.Common;
using ApiStudio.Domain.ValueObjects;

namespace ApiStudio.Domain.Entities;

public sealed class Collection : BaseEntity
{
    private readonly List<Folder> _folders = new();
    private readonly List<ApiRequest> _requests = new();

    private Collection()
    {
    }

    private Collection(
        Guid workspaceId,
        string name,string? description)
    {
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        Name = name;
        Description = description;
    }

    public Guid WorkspaceId { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; set; }

    public Workspace Workspace { get; private set; } = default!;

    public IReadOnlyCollection<Folder> Folders => _folders;

    public IReadOnlyCollection<ApiRequest> Requests => _requests;

    public static Collection Create(
        Guid workspaceId,
        string name,
        string? description)
    {
        return new Collection(workspaceId, name,description);
    }

    public Folder AddFolder(
        string name,
        Guid? parentFolderId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Folder name is required.");

        if (_folders.Any(f =>
                f.ParentFolderId == parentFolderId &&
                f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new Exception(
                "A folder with the same name already exists.");
        }

        var folder = Folder.Create(
            Id,
            name,
            parentFolderId);

        _folders.Add(folder);

        return folder;
    }

    public ApiRequest AddRequest(
        string name,
        HttpMethodType method,
        Endpoint endpoint,
        Guid? folderId)
    {
        var request = ApiRequest.Create(
            Id,
            folderId,
            name,
            method,
            endpoint);

        _requests.Add(request);

        return request;
    }
}