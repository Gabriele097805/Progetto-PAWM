using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database;
using ToDoList.Database.Models;
using ToDoList.Models;

namespace ToDoList.Services;

public class ToDoListItemService : IToDoListItemService
{
    private readonly AppDbContext m_dbContext;
    private readonly IMapper m_mapper;
    private readonly ILogger<ToDoListItemService> m_logger;

    public ToDoListItemService(
        AppDbContext dbContext,
        IMapper mapper,
        ILogger<ToDoListItemService> logger)
    {
        m_dbContext = dbContext;
        m_mapper = mapper;
        m_logger = logger;
    }

    private async Task<IEnumerable<ToDoListItemDb>> GetDbItemsAsync(int listId) =>
        await m_dbContext.ToDoListItems
            .AsNoTracking()
            .Where (i => i.List.Id == listId)
            .ToArrayAsync();

    public async Task<IEnumerable<ToDoListItem>> GetItemsAsync(int listId)
    {
        var list = await GetDbItemsAsync(listId);
        return list.Select(m_mapper.Map<ToDoListItem>);
    }

    public async Task<IEnumerable<ToDoListItem>> AddItemAsync(int userId, ToDoListItem item)
    {
        var dbItem = m_mapper.Map<ToDoListItemDb>(item);
        var list = await m_dbContext.ToDoLists
            .FirstAsync(l => l.Id == item.ListId && l.UserId == userId);

        dbItem.List = list;
        await m_dbContext.AddAsync(dbItem);
        await m_dbContext.SaveChangesAsync();
        return await GetItemsAsync(item.ListId);
    }

    public async Task<ToDoListItem?> FindItemAsync(int id)
    {
        var found = await m_dbContext.ToDoListItems.FindAsync(id);
        return m_mapper.Map<ToDoListItem>(found);
    }

    public async Task<IEnumerable<ToDoListItem>?> UpdateItemAsync(int id, ToDoListItem item)
    {
        var found = await m_dbContext.ToDoListItems.FindAsync(id);

        if (found is null)
        {
            return null;
        }

        found.Text = item.Text;
        found.Checked = item.Checked;
        m_dbContext.Update(found);
        await m_dbContext.SaveChangesAsync();
        return await GetItemsAsync(item.ListId);
    }

    public async Task<ToDoListItem?> DeleteItemAsync(int id)
    {
        var itemToDelete = await m_dbContext.ToDoListItems.FindAsync(id);

        if(itemToDelete != null)
        {
            m_dbContext.Remove(itemToDelete);
            await m_dbContext.SaveChangesAsync();
            return m_mapper.Map<ToDoListItem>(itemToDelete);
        }

        return null;
    }

    public async Task ClearListAsync(int listId)
    {
        var list = await GetDbItemsAsync(listId);
        m_dbContext.ToDoListItems.RemoveRange(list);
        await m_dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Models.ToDoList>> GetUserLists(int userId)
    {
        var dbList = await m_dbContext.ToDoLists
            .AsNoTracking()
            .Where(l => l.UserId == userId)
            .ToArrayAsync();
     
        return dbList.Select(m_mapper.Map<Models.ToDoList>);
    }
}
