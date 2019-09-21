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
}