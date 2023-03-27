using FluentValidation;

namespace Timelogger.Commands
{
    public class UpdateInvoiceValidation : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceValidation()
        {
            RuleFor(p => p.Id)
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

