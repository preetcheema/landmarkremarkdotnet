using System.Collections.Generic;
using MediatR;

namespace LandmarkRemark.BusinessLogic.Notes.Queries
{
    public class GetNotesRequest : IRequest<IEnumerable<UserNoteDetailModel>>
    {
        public string UserName { get; set; }
        public string SearchTerm { get; set; }
    }

    public class UserNoteDetailModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<NoteDetailModel> Notes { get; set; }
    }

    public class NoteDetailModel
    {
        public string Text { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Id { get; set; }
    }
}