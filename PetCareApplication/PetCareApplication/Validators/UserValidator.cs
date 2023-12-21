using FluentValidation;
using PetCareApplication.Dtos;

namespace PetCareApplication.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator() 
        { 
            RuleFor(r=>r.UserName).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.Surname).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.Email).EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(r => r.Password).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(r => r.PhoneNumber).Length(11).WithMessage("phone number must contain 11 characters");

        }
    }
}
