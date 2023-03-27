using FluentValidation;

namespace Timelogger.Commands
{
    public class GetProjectsValidation : AbstractValidator<GetProjectsCommand>
    {
        public GetProjectsValidation()
        {
            RuleFor(p => p.Id)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();
        }
    }
}

