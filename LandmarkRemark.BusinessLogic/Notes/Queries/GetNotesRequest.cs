using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemark.BusinessLogic.Notes.Queries
{
    public class GetNotesRequest : IRequest<IEnumerable<UserNoteDetailModel>>
    {
    }

    public class GetNotesRequestHandler : IRequestHandler<GetNotesRequest, IEnumerable<UserNoteDetailModel>>
    {
        private readonly LandmarkRemarkContext _context;

        public GetNotesRequestHandler(LandmarkRemarkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserNoteDetailModel>> Handle(GetNotesRequest request, CancellationToken cancellationToken)
        {
            var notes = from user in _context.Users
                where user.UserNotes.Any()
                select new UserNoteDetailModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Notes = user.UserNotes.Select(m => new NoteDetailModel
                    {
                        Id=m.Id,
                        Text = m.Text,
                        Latitude = m.Location.Y,
                        Longitude = m.Location.X
                    })
                };
            return await notes.ToListAsync(cancellationToken);
        }
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