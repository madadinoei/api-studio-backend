using FluentValidation;

namespace ApiStudio.Application.ApiRequests.Commands.SendApiRequest;

public sealed class SendApiRequestCommandValidator
    : AbstractValidator<SendApiRequestCommand>
{
    public SendApiRequestCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty();
    }
}