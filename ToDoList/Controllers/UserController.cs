using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Authentication;
using ToDoList.Models;

namespace ToDoList.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public User? Index() =>
        this.TryGetUser();
}
