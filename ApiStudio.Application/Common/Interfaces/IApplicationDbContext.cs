using ApiStudio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspaces { get; }
    DbSet<Domain.Entities.Collection> Collections { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}