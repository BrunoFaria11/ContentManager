using FluentValidation;

namespace Timelogger.Commands
{
    public class GetInvoiceValidation : AbstractValidator<GetInvoiceCommand>
    {
        public GetInvoiceValidation()
        {
            RuleFor(p => p.Id)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();
        }
    }
}

