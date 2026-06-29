using FluentValidation;

namespace ApiStudio.Application.Authentication.Commands.ProvisionUser;

public sealed class ProvisionUserCommandValidator
    : AbstractValidator<ProvisionUserCommand>
{
    public ProvisionUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}