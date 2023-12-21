using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class ActivityValidator : AbstractValidator<ActivityDto>
    {
        public ActivityValidator()
        {
            RuleFor(r=>r.ActivityName).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.PetId).NotEmpty().WithMessage("This field cannot be empty");

        }
    }
}
