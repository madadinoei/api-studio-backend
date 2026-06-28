using MediatR;

namespace ApiStudio.Application.Collection.Commands;

public record CreateCollectionCommand(
    Guid WorkspaceId,
    string Name,
    string? Description) : IRequest<Guid>;


