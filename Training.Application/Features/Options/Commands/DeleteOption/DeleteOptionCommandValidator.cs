using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionCommandValidator : AbstractValidator<DeleteOptionCommand>
    {
        public DeleteOptionCommandValidator()
        {
        
            RuleFor(q => q.OptionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");
        }
        
    }
}
