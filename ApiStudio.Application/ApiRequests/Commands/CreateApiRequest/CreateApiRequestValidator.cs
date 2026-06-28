using FluentValidation;

namespace ApiStudio.Application.ApiRequests.Commands.CreateApiRequest;

public sealed class CreateApiRequestCommandValidator
    : AbstractValidator<CreateApiRequestCommand>
{
    public CreateApiRequestCommandValidator()
    {
        RuleFor(x => x.CollectionId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Endpoint)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Method)
            .IsInEnum();
    }
}