using MediatR;

namespace ApiStudio.Application.Workspaces.Commands;

public record CreateWorkspaceCommand(
    string Name,
    string? Description) : IRequest<Guid>;