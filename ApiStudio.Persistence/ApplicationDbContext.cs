using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities;
using ApiStudio.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Persistence;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<ApiRequest> ApiRequests => Set<ApiRequest>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

//public class UserIdentityLink
//{
//    public Guid Id { get; set; }

//    public Guid UserId { get; set; }

//    public User User { get; set; } = default!;

//    public Guid IdentityUserId { get; set; }

//    public string Provider { get; set; } = default!;
//}