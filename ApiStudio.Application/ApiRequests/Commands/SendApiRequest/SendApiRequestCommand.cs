using MediatR;
using ApiStudio.HttpEngine.Abstractions.Models;

namespace ApiStudio.Application.ApiRequests.Commands.SendApiRequest;

public sealed record SendApiRequestCommand(Guid RequestId)
    : IRequest<HttpExecutionResponse>;