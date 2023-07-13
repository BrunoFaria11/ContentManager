using FluentValidation;
using ContentManager.Commands;

namespace ContentManager.Commands
{
    public class EmailValidation : AbstractValidator<EmailCommand>
    {
        public EmailValidation()
        {

            RuleFor(p => p.ToEmail)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();

            RuleFor(p => p.Body)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();
        }
    }
}

