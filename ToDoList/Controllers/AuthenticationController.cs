using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpGet("check")]
    public IActionResult CheckAuthorization()
    {
        if (Request.Cookies.Any())
        {
            return Ok();
        }

        return Unauthorized(new {
            Message = "Unauthorized"
        });
    }

    [HttpGet("proxy")]
    [Authorize]
    public IActionResult AuthorizationCallbackProxy() => Redirect("/");
}