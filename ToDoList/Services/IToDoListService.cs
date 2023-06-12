using ToDoList.Models;

namespace ToDoList.Services;

public interface IToDoListService
{
    Task<IEnumerable<Models.ToDoList>> GetListsAsync(int userId);

    Task<IEnumerable<Models.ToDoList>> AddListAsync(Models.ToDoList list, int userId);

    Task<Models.ToDoList?> FindListAsync(int id, int userId);

    Task<IEnumerable<Models.ToDoList>?> UpdateListAsync(int id, Models.ToDoList list, int userId);

    Task<IEnumerable<Models.ToDoList>?> DeleteListAsync(int id, int userId);
}
