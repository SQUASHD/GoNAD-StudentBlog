using Microsoft.AspNetCore.Mvc;

namespace StudentBlogAPI.Controllers;

[ApiController]
public class ServerUtility : ControllerBase
{
    [Route("api/v1/health")]
    [HttpGet]
    public ActionResult Health()
    {
        return Ok();
    }
}