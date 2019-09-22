using System;
using FluentValidation;
using MediatR;

namespace LandmarkRemark.BusinessLogic.Notes.Commands
{
    public class CreateNoteCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

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