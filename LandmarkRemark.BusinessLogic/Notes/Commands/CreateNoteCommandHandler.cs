using System;
using System.Threading;
using System.Threading.Tasks;
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

        public CreateNoteCommandHandler(LandmarkRemarkContext context)
        {
            _context = context;
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
                Location = new Point(request.Longitude, request.Latitude)
            };
            user.UserNotes.Add(note);

            _context.SaveChanges();
            return note.Id;
        }
    }
}