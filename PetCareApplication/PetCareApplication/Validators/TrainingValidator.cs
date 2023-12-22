using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class TrainingValidator : AbstractValidator<TrainingDto>
    {
        public TrainingValidator() 
        {
            RuleFor(r => r.TrainingName).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.PetId).NotEmpty().WithMessage("This field cannot be empty");

        }

    }
}
