using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        [HttpPost]
        public ActionResult TestInboundConnection(dynamic json)
        {   
            Console.WriteLine("hello"+json.ToString());
            return Ok("success");
        }
    }
}