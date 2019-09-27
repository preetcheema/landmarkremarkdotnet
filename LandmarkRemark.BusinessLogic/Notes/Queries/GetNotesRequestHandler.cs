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
    /// <remarks>
    /// This is a simplistic implementation where we return all the notes
    /// In real situation we may need to return only a subset of notes within the visible bounds of maps(google maps returns the coordinates of visible map area)
    /// Then also, we may need to think of lots of different scenario, for example , if map is zoomed out to a very large area
    /// we may not want to just return 10000 top notes that may belong to very small area. in that case, we may want to divide the visible area
    /// into smaller areas and then return notes from those areas.
    /// </remarks>
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
                request.SearchTerm = request.SearchTerm.Trim(); //This could be done in a filter where we can sanitize the user input
                notes = notes.Where(m => m.Text.Contains(request.SearchTerm));
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                request.UserName = request.UserName.Trim();
                notes = notes.Where(m => m.User.UserName == request.UserName);
            }

            return notes;
        }
    }
}