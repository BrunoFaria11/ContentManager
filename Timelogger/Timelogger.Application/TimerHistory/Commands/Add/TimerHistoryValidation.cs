using System.Collections.Generic;
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

            RuleFor(p => p.EndDate)
                .Must((model, field) => field == null ? true : ((model.EndDate.Value - model.StartDate).TotalMinutes > 30) ? true : false)
                .WithMessage("The minimum time is 30 minutes");
        }
    }
}

