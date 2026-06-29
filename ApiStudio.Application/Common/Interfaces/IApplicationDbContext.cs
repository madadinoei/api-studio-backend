using ApiStudio.Domain.Entities;
using ApiStudio.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspaces { get; }
    DbSet<Domain.Entities.Collection> Collections { get; }
    DbSet<ApiRequest> ApiRequests { get; }
    DbSet<User> Users { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}