using System;
using FluentValidation;

namespace LandmarkRemark.BusinessLogic.Notes.Commands
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        /// <summary>
        /// validates create note command. 0 and 0 are valid latitude and longitude
        /// This validator might need more thought as default value for latitude and longitude is 0
        /// We may make latitude and longitude as nullable and then explicitly validate for null values in validator.
        /// </summary>
        /// <param name="userExists"></param>
        public CreateNoteCommandValidator(Func<int,bool>userExists)
        {
            RuleFor(m => m.Text).NotEmpty().MaximumLength(50);
            RuleFor(m => m.UserId).Must(m => userExists(m));
            RuleFor(m => m.Latitude).InclusiveBetween(-90, 90);
            RuleFor(m => m.Longitude).InclusiveBetween(-180, 180);
        }
    }
}