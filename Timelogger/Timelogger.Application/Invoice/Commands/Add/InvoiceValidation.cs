using FluentValidation;
using Timelogger.Commands;

namespace Timelogger.Commands
{
    public class InvoiceValidation : AbstractValidator<InvoiceCommand>
    {
        public InvoiceValidation()
        {
            RuleFor(p => p.ProjectId)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();

            RuleFor(p => p.DevName)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();

            RuleFor(p => p.DevDocNumber)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();

            RuleFor(p => p.CustomerName)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();

            RuleFor(p => p.CustomerDocNumber)
            .NotEmpty().WithMessage("{PropertyName} required.")
            .NotNull();
        }
    }
}

