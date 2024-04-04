
using Application.Command.UsersCommand;
using Domains.repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Validation
{
    public class RegisterValidator : AbstractValidator<CreateUserCommand>
    {
        public readonly IEmailExistsValidator _emailValidator;

        public RegisterValidator(IEmailExistsValidator emailValidator)
        {
            _emailValidator = emailValidator;


            RuleFor(x => x.email)
                .EmailAddress().WithMessage("A valid email address is required.")
                .NotEmpty().WithMessage("Email is required")
                //it is added to validate if user email already exist in db or not
                .Must((email) => !_emailValidator.UserExists(email)).WithMessage("Email already exists in the database");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{ Property name} should never be empty")
                .Length(3, 15).WithMessage("Name be greater than 3 character length and less than 15.")
                .Must(IsValidName).WithMessage("Name shouldnot contain any digit or symbol");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{ Property name} should never be empty")
                .MinimumLength(8).WithMessage("Password must be 8 character long")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d+").WithMessage("Password must contain at least one digit.")
                .Matches(@"[!@#$%^&*()_+{}|:;<>,.?~]+").WithMessage("Password must contain at least one special character.")
                .Length(8, 15);

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Enum constant must be applied")
            .IsInEnum().WithMessage("Invalid order status.");
        }
        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }
    }
}
