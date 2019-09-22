using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LandmarkRemark.BusinessLogic.Infrastructure;
using LandmarkRemark.Common;
using LandmarkRemark.Domain.Entities;
using LandmarkRemark.Persistence;
using MediatR;

namespace LandmarkRemark.BusinessLogic.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,int>
    {
        private readonly LandmarkRemarkContext _context;
        private readonly ITimeProvider _timeProvider;

        public CreateUserCommandHandler(LandmarkRemarkContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator((username) => !_context.Users.Any(x => x.UserName == username));
            validator.ValidateAndThrowUnProcessableEntityException(request);
            
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                CreatedOn = _timeProvider.Now()
            };

            (byte[] passwordHash, byte[] passwordSalt) = CreatePasswordHash(request.Password);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        private (byte[]passwordHash, byte[]passwordSalt) CreatePasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }
    }
}