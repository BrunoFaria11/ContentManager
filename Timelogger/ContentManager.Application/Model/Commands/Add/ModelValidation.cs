using System.Collections.Generic;
using FluentValidation;
using ContentManager.Commands;

namespace ContentManager.Commands
{
    public class ModelValidation : AbstractValidator<ModelCommand>
    {

        public ModelValidation()
        {

            RuleFor(p => p.ApplicationId)
             .NotEmpty().WithMessage("{PropertyName} required.")
             .NotNull();

            RuleFor(p => p.Value)
                         .NotEmpty().WithMessage("{PropertyName} required.")
                         .NotNull();

        }
    }
}

