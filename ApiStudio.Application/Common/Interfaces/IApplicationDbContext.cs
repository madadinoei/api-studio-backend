using ApiStudio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiStudio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspaces { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}