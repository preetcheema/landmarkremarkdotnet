using System;
using FluentValidation;

namespace LandmarkRemark.BusinessLogic.Notes.Commands
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator(Func<int,bool>userExists)
        {
            RuleFor(m => m.Text).NotEmpty().MaximumLength(50);
            RuleFor(m => m.UserId).Must(m => userExists(m));
            RuleFor(m => m.Latitude).InclusiveBetween(-90, 90);
            RuleFor(m => m.Longitude).InclusiveBetween(-180, 180);
        }
    }
}