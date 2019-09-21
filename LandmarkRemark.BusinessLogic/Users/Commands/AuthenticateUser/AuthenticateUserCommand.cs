using System;
using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.BusinessLogic.Infrastructure;
using LandmarkRemark.BusinessLogic.Users.Queries;
using LandmarkRemark.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemark.BusinessLogic.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<UserDetailModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserDetailModel>
    {
        private readonly LandmarkRemarkContext _context;

        public AuthenticateUserCommandHandler(LandmarkRemarkContext context)
        {
            _context = context;
        }

        public async Task<UserDetailModel> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AuthenticateUserCommandValidator();
            validator.ValidateAndThrowUnProcessableEntityException(request);

            var user = await _context.Users.SingleOrDefaultAsync(m => m.UserName == request.UserName, cancellationToken: cancellationToken);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            
            return UserDetailModel.Create(user);

        }
        
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}