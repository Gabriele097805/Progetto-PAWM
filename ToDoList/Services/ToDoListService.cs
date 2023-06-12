using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database;
using ToDoList.Database.Models;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly AppDbContext m_dbContext;
        private readonly IMapper m_mapper;
        private readonly ILogger<ToDoListService> m_logger;

        public ToDoListService(
            AppDbContext dbContext,
            IMapper mapper,
            ILogger<ToDoListService> logger)
        {
            m_dbContext = dbContext;
            m_mapper = mapper;
            m_logger = logger;
        }

        private async Task<IEnumerable<Models.ToDoList>> GetUserListsAsync(int userId)
        {
            var dbList = await m_dbContext.ToDoLists
                .AsNoTracking()
                .Where(l => l.UserId == userId)
                .ToArrayAsync();

            return dbList.Select(m_mapper.Map<Models.ToDoList>);
        }

        public async Task<IEnumerable<Models.ToDoList>> GetListsAsync(int userId)
        {
            var lists = await GetUserListsAsync(userId);
            return lists.Select(m_mapper.Map<Models.ToDoList>);
        }

        public async Task<IEnumerable<Models.ToDoList>> AddListAsync(Models.ToDoList list, int userId)
        {
            var dbList = m_mapper.Map<ToDoListDb>(list);
            dbList.UserId = userId;
            await m_dbContext.AddAsync(dbList);
            await m_dbContext.SaveChangesAsync();
            return await GetListsAsync(userId);
        }

        public async Task<Models.ToDoList?> FindListAsync(int id, int userId)
        {
            var found = await m_dbContext.ToDoLists.FindAsync(id);

            if (found != null && found.UserId == userId)
            {
                return m_mapper.Map<Models.ToDoList>(found);
            }

            return null;
        }

        public async Task<IEnumerable<Models.ToDoList>?> UpdateListAsync(int id, Models.ToDoList list, int userId)
        {
            var listToUpdate = await m_dbContext.ToDoLists.FindAsync(id);

            if (listToUpdate is null || listToUpdate.UserId != userId)
            {
                return null;
            }

            listToUpdate.Name = list.Name;
            m_dbContext.Update(listToUpdate);
            await m_dbContext.SaveChangesAsync();
            return await GetListsAsync(listToUpdate.UserId);
        }

        public async Task<IEnumerable<Models.ToDoList>?> DeleteListAsync(int id, int userId)
        {
            var listToDelete = await m_dbContext.ToDoLists.FindAsync(id);

            if (listToDelete != null && listToDelete.UserId == userId)
            {
                m_dbContext.Remove(listToDelete);
                await m_dbContext.SaveChangesAsync();
                return await GetListsAsync(userId);
            }

            return null;
        }
    }
}
