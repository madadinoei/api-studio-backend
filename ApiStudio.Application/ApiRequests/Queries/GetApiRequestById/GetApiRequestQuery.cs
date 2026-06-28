using ApiStudio.Application.ApiRequests.Dtos;
using ApiStudio.Application.Workspaces.Queries.GetWorkspaceById;
using MediatR;

namespace ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;

public sealed record GetApiRequestQuery(Guid Id)
    : IRequest<ApiRequestDto?>, IRequest<WorkspaceDto?>;