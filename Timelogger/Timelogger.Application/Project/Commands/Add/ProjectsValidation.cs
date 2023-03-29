using System;
using FluentValidation;

namespace Timelogger.Commands
{
    public class ProjectsValidation : AbstractValidator<ProjectsCommand>
    {
        public ProjectsValidation()
        {
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();

            RuleFor(p => p.DeadLine)
                         .NotEmpty().WithMessage("{PropertyName} required.")
                         .NotNull()
                         .Must(x => x > DateTime.Now).WithMessage("{PropertyName} must be after today.");


            RuleFor(p => p.TimePerWeek)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .NotNull();
        }
    }
}

