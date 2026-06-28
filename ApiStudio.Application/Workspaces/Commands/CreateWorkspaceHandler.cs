using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities;
using MediatR;

namespace ApiStudio.Application.Workspaces.Commands;

public class CreateWorkspaceHandler
    : IRequestHandler<CreateWorkspaceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateWorkspaceHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var workspace = new Workspace(
            request.Name,
            request.Description);

        _context.Workspaces.Add(workspace);

        await _context.SaveChangesAsync(cancellationToken);

        return workspace.Id;
    }
}