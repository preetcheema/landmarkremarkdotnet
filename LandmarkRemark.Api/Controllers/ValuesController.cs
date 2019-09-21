using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.Api.Controllers
{
    public class Note
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Text { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public ActionResult<string> Get(int Id)
        {
            return "value";
        }
    }
}