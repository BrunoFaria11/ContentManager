using FluentValidation;
using Timelogger.Commands;

namespace Timelogger.Commands
{
    public class GetAllTimerHistoryValidation : AbstractValidator<GetAllTimerHistoryCommand>
    {
        public GetAllTimerHistoryValidation()
        {
            RuleFor(p => p.ProjectId)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .NotNull();
        }
    }
}

