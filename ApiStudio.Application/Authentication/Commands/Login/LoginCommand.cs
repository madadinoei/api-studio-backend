using ApiStudio.Application.Authentication.Dtos;
using MediatR;

namespace ApiStudio.Application.Authentication.Commands.Login;

public sealed record LoginCommand(
    string UserName,
    string Password)
    : IRequest<LoginResponse>;