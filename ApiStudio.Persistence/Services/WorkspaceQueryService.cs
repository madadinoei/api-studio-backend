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
                        Type = TreeNodeType.Folder.ToString().ToLower(),
                        Children = []
                    });
                var rootNodes = new List<TreeNodeDto>();

                foreach (var folder in folders)
                {
                    var node = folderLookup[folder.Id];

                    if (folder.ParentFolderId == null)
                    {
                        node.Children = [];
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
                        Type = TreeNodeType.Request.ToString().ToLower(),
                        Method = request.Method.ToString().ToUpper(),
                        Expanded = true,
                        RequestId = request.Id.ToString(),
                        Children = []
                    };

                    if (request.FolderId == null)
                    {
                        rootNodes.Add(node);
                    }
                    else
                    {
                        // اگر شناسه فولدر داشت، آن را در دیکشنری فولدرها پیدا کرده و به لیست فرزندانش اضافه می‌کنیم
                        if (folderLookup.TryGetValue(request.FolderId.Value, out var parentFolder))
                        {
                            parentFolder.Children.Add(node);
                        }
                        else
                        {
                            // در صورتی که به هر دلیلی فولدر والد پیدا نشد (مثلا خطای دیتابیسی یا حذف منطقی)
                            // تصمیم با شماست؛ می‌توانید آن را به ریشه اضافه کنید یا نادیده بگیرید.
                            rootNodes.Add(node);
                        }


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
