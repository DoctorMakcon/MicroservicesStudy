using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        [HttpPost]
        public void Post()
        {
            Console.WriteLine("---> Inbound POST request");
        }
    }
}
