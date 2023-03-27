using FluentValidation;

namespace Timelogger.Commands
{
    public class GetTimerHistoryValidation : AbstractValidator<GetTimerHistoryCommand>
    {
        public GetTimerHistoryValidation()
        {
            RuleFor(p => p.Id)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();
        }
    }
}

