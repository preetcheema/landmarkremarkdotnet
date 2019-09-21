using FluentValidation;
using FluentValidation.Results;

namespace LandmarkRemark.BusinessLogic.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public override ValidationResult Validate(ValidationContext<AuthenticateUserCommand> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] {new ValidationFailure(typeof(AuthenticateUserCommand).Name, "Command cannot be null")})
                : base.Validate(context);
        }

        public AuthenticateUserCommandValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty()
                .Must(m=>!m.Contains(" "))
                .WithMessage("Username must be provided");
            RuleFor(m => m.Password)
                .NotEmpty()
                .Must(m=>!m.Contains(" "))
                .WithMessage("Password must be provided");
        }
    }
}