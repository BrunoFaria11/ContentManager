using FluentValidation;
using ContentManager.Commands;

namespace ContentManager.Commands
{
    public class GetAllModelsValidation : AbstractValidator<GetAllModelsCommand>
    {
        public GetAllModelsValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .NotNull();
        }
    }
}

