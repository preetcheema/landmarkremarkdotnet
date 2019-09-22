using System;
using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.Common;
using LandmarkRemark.Domain.Entities;
using LandmarkRemark.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace LandmarkRemark.BusinessLogic.Notes.Commands
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly LandmarkRemarkContext _context;
        private readonly ITimeProvider _timeProvider;

        public CreateNoteCommandHandler(LandmarkRemarkContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        
        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == request.UserId);
            if (user == null)
            {
                throw new Exception("Fdas");
            }

            var note = new Note
            {
                Text = request.Text,
                Location = new Point(request.Longitude, request.Latitude),
                AddedOn = _timeProvider.Now()
            };
            user.UserNotes.Add(note);

            _context.SaveChanges();
            return note.Id;
        }
    }
}