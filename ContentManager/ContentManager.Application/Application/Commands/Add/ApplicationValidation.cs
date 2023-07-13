using FluentValidation;
using ContentManager.Commands;

namespace ContentManager.Commands
{
    public class ApplicationValidation : AbstractValidator<ApplicationCommand>
    {
        public ApplicationValidation()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();
        }
    }
}

