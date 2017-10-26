namespace PrettyLink.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiExplorerSettings(IgnoreApi = true)]
    public class HealthCheckController : Controller
    {
        [HttpGet("/healthcheck")]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetHealthCheck()
        {
            return Ok("Hello world");
        }
    }
}
