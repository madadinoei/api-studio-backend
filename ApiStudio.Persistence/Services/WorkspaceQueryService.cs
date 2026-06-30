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

        public async Task<List<CollectionDto>> GetWorkspaceCollectionsAsync(Guid id)
        {
            var workspace = await _applicationDbContext.Workspaces.Include(x => x.Collections).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (workspace is null)
            {
                throw new Exception("not found");
            }

            return workspace.Collections.Select(x => new CollectionDto()
            {
                Name = x.Name,
                Description = x.Description,
                Nodes = []
            }).ToList();
        }
    }
}
