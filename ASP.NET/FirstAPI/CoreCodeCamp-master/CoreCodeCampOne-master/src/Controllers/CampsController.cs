using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreCodeCamp.Controllers
{
    //[controller] says whatever comes before the world controller is the route
    [Route("api/[controller]")]
    public class CampsController : ControllerBase
    {

        //actions are the endpoint of requests
        [HttpGet]
        public IActionResult GetCamps()
        {
            return Ok(new { Moniker = "ATL2018", Name = "Atlanta Code Camp" });
        }
    }
}
