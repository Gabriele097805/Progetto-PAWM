using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Authentication;
using ToDoList.Dtos;
using ToDoList.Services;

namespace ToDoList.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]

public class ToDoListController : ControllerBase
{
    private readonly IToDoListService m_toDoListService;
    private readonly IMapper m_mapper;
    private readonly ILogger<ToDoListController> m_logger;

    public ToDoListController(
        IToDoListService toDoListService,
        IMapper mapper,
        ILogger<ToDoListController> logger)
    {
        m_toDoListService = toDoListService;
        m_mapper = mapper;
        m_logger = logger;
    }

    [Authorize]
    [HttpGet("lists")]
    public async Task<IEnumerable<ToDoListDto>> GetLists()
    {
        var lists = await m_toDoListService.GetListsAsync(this.GetUser().Id);
        return lists.Select(m_mapper.Map<ToDoListDto>);
    }

    [Authorize]
    [HttpPost("add-list")]
    public async Task<IEnumerable<ToDoListDto>> AddList([FromBody] Models.ToDoList list)
    {
        var lists = await m_toDoListService.AddListAsync(list, this.GetUser().Id);
        return lists.Select(m_mapper.Map<ToDoListDto>);
    }

    [Authorize]
    [HttpGet("find-list")]
    public async Task<ActionResult<Models.ToDoList>> FindList(int id)
    {
        var found = await m_toDoListService.FindListAsync(id, this.GetUser().Id);

        if (found == null)
        {
            return NotFound();
        }

        return Ok(found);
    }

    [Authorize]
    [HttpPut("update-list")]
    public async Task<ActionResult<IEnumerable<Models.ToDoList>>> UpdateList([FromBody] Models.ToDoList list, [FromQuery] int id)
    {
        var found = await m_toDoListService.UpdateListAsync(id, list, this.GetUser().Id);

        if (found == null)
        {
            return NotFound();
        }

        return Ok(found);
    }

    [Authorize]
    [HttpDelete("delete-list")]
    public Task<IEnumerable<Models.ToDoList>?> DeleteList(int id) =>
        m_toDoListService.DeleteListAsync(id, this.GetUser().Id);

}
