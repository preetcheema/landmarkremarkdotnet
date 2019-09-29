using System.Collections.Generic;
using System.Threading.Tasks;
using LandmarkRemark.Api.Models;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Notes.Commands;
using LandmarkRemark.BusinessLogic.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNoteDetailModel>>> Get(string username, string searchTerm)
        {
            var result = await _mediator.Send(new GetNotesRequest {UserName = username, SearchTerm = searchTerm});

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNoteViewModel model)
        {
            //This could be added into global filter if null model validation is being checked at multiple places
            if (model == null)
            {
                return BadRequest();
            }
            var userId = int.Parse(User.Identity.Name);
            var result = await _mediator.Send(new CreateNoteCommand {UserId = userId, Text = model.Text, Latitude = model.Latitude, Longitude = model.Longitude});
            return Ok(result);
        }
    }
}