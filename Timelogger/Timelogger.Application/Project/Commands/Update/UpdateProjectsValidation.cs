using FluentValidation;

namespace Timelogger.Commands
{
    public class UpdateProjectsValidation : AbstractValidator<UpdateProjectsCommand>
    {
        public UpdateProjectsValidation()
        {
            RuleFor(p => p.Id)
                    .NotEmpty().WithMessage("{PropertyName} required.")
                    .NotNull();

            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();

            RuleFor(p => p.DeadLine)
                         .NotEmpty().WithMessage("{PropertyName} required.")
                         .NotNull();

            RuleFor(p => p.TimePerWeek)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .NotNull();

            RuleFor(p => p.IsCompleted)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .NotNull();
        }
    }
}

