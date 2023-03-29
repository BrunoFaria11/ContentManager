using FluentValidation;
using Timelogger.Commands;

namespace Timelogger.Commands
{
    public class GetAllInvoiceValidation : AbstractValidator<GetAllInvoiceCommand>
    {
        public GetAllInvoiceValidation()
        {
            RuleFor(p => p.ProjectId)
                   .NotEmpty().WithMessage("{PropertyName} required.")
                   .NotNull();
        }
    }
}

