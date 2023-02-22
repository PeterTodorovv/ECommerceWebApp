using e_Commerce.Data.Entities;
using e_Commerce.Services.Requests;
using FluentValidation;

namespace e_Commerce_Website.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name can not be empty.")
                .NotNull().WithMessage("First name can not be empty.")
                .MinimumLength(2).WithMessage("First name can not be less then 2 characters long.")
                .MaximumLength(20).WithMessage("First name can not be more then 20 characters long");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name can not be empty.")
                .MinimumLength(2).WithMessage("Last name can not be less then 2 characters long.")
                .MaximumLength(30).WithMessage("Last name can not be more then 30 characters long");

            RuleFor(u => u.Username).NotEmpty().WithMessage("Username can not be empty.")
                .MinimumLength(6).WithMessage("Username can not be less then 6 characters long.")
                .MaximumLength(16).WithMessage("Username can not be more then 16 characters long.");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email can not be empty.")
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage("Ivalid email format.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password can not be empty.")
                .MinimumLength(6).WithMessage("Password can not be less then 6 characters long.")
                .MaximumLength(20).WithMessage("Password can not be more then 20 characters long.");
        }
    }
}
