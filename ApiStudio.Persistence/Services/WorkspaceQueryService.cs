using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiStudio.Application.Collection.Dtos;
using ApiStudio.Application.Workspaces.Dtos;
using ApiStudio.Application.Workspaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Persistence.Services
{
    public class WorkspaceQueryService : IWorkspaceQueryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WorkspaceQueryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<WorkspaceDto>> GetWorkspaceListQuery()
        {
            return await _applicationDbContext.Workspaces
                .AsNoTracking()
                .Select(x => new WorkspaceDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();
        }

        public async Task<WorkspaceDto?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.Workspaces
                .AsNoTracking()
                .Select(x => new WorkspaceDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CollectionTreeDto>> GetWorkspaceCollectionsAsync(Guid id)
        {
            var workspace = await _applicationDbContext.Workspaces.Include(x => x.Collections).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (workspace is null)
            {
                throw new Exception("not found");
            }

            var result = new List<CollectionTreeDto>();
            foreach (var collection in workspace.Collections)
            {
                var folders = await _applicationDbContext.Folders
                    .Where(x => x.CollectionId == collection.Id)
                    .ToListAsync();

                var requests = await _applicationDbContext.ApiRequests
                    .Where(x => x.CollectionId == collection.Id)
                    .ToListAsync();

                var folderLookup = folders.ToDictionary(
                    x => x.Id,
                    x => new TreeNodeDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = TreeNodeType.Folder.ToString().ToLower()
                    });
                var rootNodes = new List<TreeNodeDto>()
                {
                    new()
                    {

                        Id = Guid.NewGuid(),
                        Name = "Get Madadi",
                        Type = TreeNodeType.Request.ToString().ToLower(),
                        Method = "PUT",
                        RequestId = String.Empty,
                        Expanded = true,
                        Children = []
                    }
                };

                foreach (var folder in folders)
                {
                    var node = folderLookup[folder.Id];

                    if (folder.ParentFolderId == null)
                    {
                        node.Children =
                        [
                            new TreeNodeDto()
                            {

                                Id = Guid.NewGuid(),
                                Name = "Get All",
                                Type = TreeNodeType.Request.ToString().ToLower(),
                                Method = "GET",
                                RequestId = String.Empty,
                                Expanded = true,
                                Children = []
                            }

                        ];
                        rootNodes.Add(node);

                    }
                    else if (folderLookup.TryGetValue(folder.ParentFolderId.Value, out var parent))
                    {
                        parent.Children.Add(node);
                        
                    }

                }

                foreach (var request in requests)
                {
                    var node = new TreeNodeDto
                    {
                        Id = request.Id,
                        Name = request.Name,
                        Type = TreeNodeType.Request.ToString(),
                    };

                    if (request.FolderId == null)
                    {
                        rootNodes.Add(node);
                    }
                    else if (folderLookup.TryGetValue(request.FolderId.Value, out var folder))
                    {
                        folder.Children.Add(node);
                        
                    }
                }
                var dto = new CollectionTreeDto
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    Nodes = rootNodes
                };
                result.Add(dto);
            }



            return result;
        }
    }
}
