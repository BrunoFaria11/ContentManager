using FluentValidation;

namespace Timelogger.Commands
{
    public class UpdateTimerHistoryValidation : AbstractValidator<UpdateTimerHistoryCommand>
    {
        public UpdateTimerHistoryValidation()
        {
            RuleFor(p => p.Id)
                    .NotEmpty().WithMessage("{PropertyName} required.")
                    .NotNull();

            RuleFor(p => p.EndDate)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();
        }
    }
}

