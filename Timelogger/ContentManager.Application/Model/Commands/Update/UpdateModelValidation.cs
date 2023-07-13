using FluentValidation;

namespace ContentManager.Commands
{
    public class UpdateModelValidation : AbstractValidator<UpdateModelCommand>
    {
        public UpdateModelValidation()
        {
            RuleFor(p => p.Id)
                    .NotEmpty().WithMessage("{PropertyName} required.")
                    .NotNull();

            RuleFor(p => p.Value)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();
        }
    }
}

