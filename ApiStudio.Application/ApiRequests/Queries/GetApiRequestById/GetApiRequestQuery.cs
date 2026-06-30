using ApiStudio.Application.ApiRequests.Dtos;
using ApiStudio.Application.Workspaces.Dtos;
using MediatR;

namespace ApiStudio.Application.ApiRequests.Queries.GetApiRequestById;

public sealed record GetApiRequestQuery(Guid Id)
    : IRequest<ApiRequestDto?>, IRequest<WorkspaceDto?>;