using ApiStudio.Application.Collection.Dtos;
using ApiStudio.Application.Workspaces.Dtos;

namespace ApiStudio.Application.Workspaces.Interfaces;

public interface IWorkspaceQueryService
{
    Task<List<WorkspaceDto>> GetWorkspaceListQuery();
    Task<WorkspaceDto?> GetByIdAsync(Guid id);

    Task<List<CollectionTreeDto>> GetWorkspaceCollectionsAsync(Guid id);
}