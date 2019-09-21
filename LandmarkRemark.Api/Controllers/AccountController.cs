using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LandmarkRemark.Api.Infrastructure;
using LandmarkRemark.BusinessLogic.Users.Commands.AuthenticateUser;
using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LandmarkRemark.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountController(IMediator mediator, IOptions<AuthenticationSettings> authenticationSettings)
        {
            _mediator = mediator;
            _authenticationSettings = authenticationSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand user)
        {
            try
            {
                await _mediator.Send(new CreateUserCommand {FirstName = user.FirstName, LastName = user.LastName, Password = user.Password, Username = user.Username});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand request)
        {
            var user = await _mediator.Send(new AuthenticateUserCommand {UserName = request.UserName, Password = request.Password});

            if (user == null)
            {
                return BadRequest(new {message = "Incorrect username or password"});
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
    }
}