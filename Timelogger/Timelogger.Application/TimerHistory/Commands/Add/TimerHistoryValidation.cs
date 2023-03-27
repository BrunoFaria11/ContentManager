using FluentValidation;
using Timelogger.Commands;

namespace Timelogger.Commands
{
    public class TimerHistoryValidation : AbstractValidator<TimerHistoryCommand>
    {
        public TimerHistoryValidation()
        {
            RuleFor(p => p.ProjectId)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();

            RuleFor(p => p.StartDate)
                         .NotEmpty().WithMessage("{PropertyName} required.")
                         .NotNull();
        }
    }
}

