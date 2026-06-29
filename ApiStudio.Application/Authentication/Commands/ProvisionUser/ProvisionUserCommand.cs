using MediatR;

namespace ApiStudio.Application.Authentication.Commands.ProvisionUser;

public sealed record ProvisionUserCommand(
    string UserName,
    string Password) : IRequest;