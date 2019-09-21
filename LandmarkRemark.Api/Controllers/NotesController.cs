using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<UserNoteDetailModel>>> Get()
        {

            var result = await _mediator.Send(new GetNotesRequest());
         
//            var list = new List<Note>
//            {
//                new Note {Id = 1, UserName = "Preet", Latitude = -37.9131133m, Longitude = 145.1257112m, Text = "this is sample Text 1"},
//                new Note {Id = 2, UserName = "Johnny", Latitude = -27.4640302m, Longitude = 153.0272569m, Text = "great place"},
//                new Note {Id = 3, UserName = "Mark", Latitude = -27.4522202m, Longitude = 152.9986068m, Text = "really good food"},
//                new Note {Id = 4, UserName = "Carly", Latitude = -27.6012352m, Longitude = 152.7048727m, Text = "this is an awesome place really really good to visit"},
//                new Note {Id = 5, UserName = "Sona", Latitude = -27.1826568m, Longitude = 151.2666041m, Text = "hmm its ok nothing great"},
//                new Note {Id = 6, UserName = "Jhanvi", Latitude = -37.8173499m, Longitude = 144.8847535m, Text = "fabulous"},
//                new Note {Id = 7, UserName = "Bindu", Latitude = -36.8366098m, Longitude = 139.8541616m, Text = "must visit if you are going here"},
//                new Note {Id = 8, UserName = "Naveen", Latitude = -31.8116919m, Longitude = 115.7418046m, Text = "if on way recommended visit"},
//                new Note {Id = 9, UserName = "Anil", Latitude = 19.0666334m, Longitude = 72.9248994m, Text = "try if you"},
//                new Note {Id = 10, UserName = "Shiv", Latitude = 19.7959601m, Longitude = 73.0950647m, Text = "go if you ca"}
//            };

            return Ok(result);
        }
    }
}