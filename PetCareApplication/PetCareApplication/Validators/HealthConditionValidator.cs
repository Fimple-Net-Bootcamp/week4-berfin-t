using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class HealthConditionValidator : AbstractValidator<HealthConditionDto>
    {
        public HealthConditionValidator() 
        {
            RuleFor(r => r.Diagnosis).NotEmpty().WithMessage("This field cannot be empty");

        }
    }
}
