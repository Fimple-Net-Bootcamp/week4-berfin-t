using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class PetValidator : AbstractValidator<PetDto>
    {
        public PetValidator() 
        { 
            RuleFor(r=>r.PetName).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.Kind).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.Age).GreaterThan(0).WithMessage("Age must be greater than 0");
            RuleFor(r => r.Gender).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.UserId).NotEmpty().WithMessage("This field cannot be empty");

        }
    }
}
