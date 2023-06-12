using ToDoList.Models;

namespace ToDoList.Services;

public interface IToDoListItemService
{
    Task<IEnumerable<ToDoListItem>> GetItemsAsync(int listId);

    Task<IEnumerable<ToDoListItem>> AddItemAsync(int userId, ToDoListItem item);

    Task<ToDoListItem?> FindItemAsync(int id);

    Task<IEnumerable<ToDoListItem>?> UpdateItemAsync(int id, ToDoListItem item);

    Task<ToDoListItem?> DeleteItemAsync(int id);

    Task ClearListAsync(int listId);
}
