using MediatR;

namespace ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;

public record GetWorkspaceByIdQuery(Guid Id) : IRequest<WorkspaceDto?>;