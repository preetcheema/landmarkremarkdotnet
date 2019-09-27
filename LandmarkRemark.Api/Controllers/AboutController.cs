using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Landmark Remark Api";
        }
      }
}