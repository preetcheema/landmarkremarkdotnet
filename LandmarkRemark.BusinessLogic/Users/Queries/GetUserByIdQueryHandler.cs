using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.Domain.Entities;
using LandmarkRemark.Persistence;
using MediatR;

namespace LandmarkRemark.BusinessLogic.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailModel>
    {
        private readonly LandmarkRemarkContext _context;

        public GetUserByIdQueryHandler(LandmarkRemarkContext context)
        {
            _context = context;
        }

        public async Task<UserDetailModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), request.Id);
            }

            return UserDetailModel.Create(user);
        }
    }
}