using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.BusinessLogic.Dtos.User;

namespace Todo.API.Validators.User
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {

        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please provide a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }

}
