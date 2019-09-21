using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.Domain.Entities;
using LandmarkRemark.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemark.BusinessLogic.Notes.Queries
{
    public class GetNotesRequest : IRequest<IEnumerable<UserNoteDetailModel>>
    {
        public string UserName { get; set; }
        public string SearchTerm { get; set; }
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
            var notes = _context.Notes.AsQueryable();

            notes = ApplyQueryFilter(request, notes);

            var result = from note in notes
                group note by new{note.User.Id,note.User.UserName}
                into grp
                select new UserNoteDetailModel
                {
                    UserId = grp.Key.Id,
                    UserName = grp.Key.UserName,
                    Notes=grp.Select(m=>new NoteDetailModel
                    {
                        Id=m.Id,
                        Text = m.Text,
                        Latitude = m.Location.Y,
                        Longitude = m.Location.X
                    })
                };


              return await result.ToListAsync(cancellationToken);
        }

        private static IQueryable<Note> ApplyQueryFilter(GetNotesRequest request, IQueryable<Note> notes)
        {
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                notes = notes.Where(m => m.Text.Contains(request.SearchTerm));
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                notes = notes.Where(m => m.User.UserName == request.UserName);
            }

            return notes;
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