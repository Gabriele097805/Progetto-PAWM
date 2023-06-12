using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Authentication;
using ToDoList.Dtos;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ToDoListItemsController : ControllerBase
{
    private readonly IToDoListItemService m_toDoListItemService;
    private readonly IMapper m_mapper;
    private readonly ILogger<ToDoListItemsController> m_logger;

    public ToDoListItemsController(
        IToDoListItemService toDoListItemService,
        IMapper mapper,
        ILogger<ToDoListItemsController> logger)
    {
        m_toDoListItemService = toDoListItemService;
        m_mapper = mapper;
        m_logger = logger;
    }

    [Authorize]
    [HttpGet("{listId}")]
    public async Task<IEnumerable<ToDoListItemDto>> GetItems([FromRoute] int listId)
    {
        var list = await m_toDoListItemService.GetItemsAsync(listId);
        return list.Select(m_mapper.Map<ToDoListItemDto>);
    }

    [Authorize]
    [HttpPost("{listId}")]
    public async Task<IEnumerable<ToDoListItem>> AddItem([FromRoute] int listId, [FromBody] ToDoListItem item) =>
        await m_toDoListItemService.AddItemAsync(this.GetUser()!.Id, item with { ListId = listId });

    [Authorize]
    [HttpGet("find/{id}")]
    public async Task<ActionResult<ToDoListItem>> FindItem(int id)
    {
        var found = await m_toDoListItemService.FindItemAsync(id);

        if (found == null)
        {
            return NotFound();
        }

        return Ok(found);
    }

    [Authorize]
    [HttpPut("{listId}")]
    public async Task<ActionResult<IEnumerable<ToDoListItem>>> UpdateItem([FromRoute] int listId, [FromBody] ToDoListItem item)
    {
        var found = await m_toDoListItemService.UpdateItemAsync(listId, item);

        if (found == null)
        {
            return NotFound();
        }

        return Ok(found);
    }

    [Authorize]
    [HttpDelete("{listId}")]
    public Task<ToDoListItem?> DeleteItem(int id) =>
        m_toDoListItemService.DeleteItemAsync(id);

    [HttpDelete("{listId}/clear")]
    public Task ClearList([FromRoute] int listId) =>
        m_toDoListItemService.ClearListAsync(listId);
}
