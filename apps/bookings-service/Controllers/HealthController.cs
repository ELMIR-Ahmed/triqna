using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new 
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                code = 200
            };
            return Ok(response);
        }
    }
}