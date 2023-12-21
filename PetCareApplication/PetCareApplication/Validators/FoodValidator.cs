using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class FoodValidator : AbstractValidator<FoodDto>
    {
        public FoodValidator() 
        {
            RuleFor(r => r.FoodName).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.FoodType).NotEmpty().WithMessage("This field cannot be empty");

        }
    }
}
