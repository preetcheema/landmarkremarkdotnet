using System;
using FluentValidation;
using FluentValidation.Results;

namespace LandmarkRemark.BusinessLogic.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public override ValidationResult Validate(ValidationContext<CreateUserCommand> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] {new ValidationFailure(typeof(CreateUserCommand).Name, "Command cannot be null")})
                : base.Validate(context);
        }

        public CreateUserCommandValidator(Func<string, bool> userNameDoesNotExist)
        {
            RuleFor(m => m.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("First name must be provided with maximum length of 100");

            RuleFor(m => m.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("First name must be provided with maximum length of 100");

            RuleFor(m => m.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(100)
                .Must(m => !m.Contains(" "))
                .Must(userNameDoesNotExist)
                .WithMessage("User name must be uniqie, maximum length of 50 and no spaces");

            RuleFor(m => m.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(15)
                .Must(m => !m.Contains(" "))
                .WithMessage("Password must be provided with maximum length of 15 and no spaces");
        }
    }
}