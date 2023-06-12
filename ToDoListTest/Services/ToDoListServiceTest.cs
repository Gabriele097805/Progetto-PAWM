using Microsoft.Extensions.Logging.Abstractions;
using ToDoList.Models;
using ToDoList.Services;
using ToDoListTest.Mocks;

namespace ToDoListTest.Services;

[TestClass]
public class ToDoListServiceTest
{
    private ToDoListItem GetDummyToDoListItem() => new(1, 1, true, "text");
    
    [TestMethod]
    public async Task GetItemsAsyncTest()
    {
        var dbContext = MocksExtensions.GetInMemoryDbContext();
        var mapper = MocksExtensions.AddMapper();

        var dummyListId = 1;
        var dummyUserId = 1;
        var listService = new ToDoListService(dbContext, mapper, new NullLogger<ToDoListService>());
        await listService.AddListAsync(new(dummyListId, dummyUserId, "Some Name"), dummyUserId);

        var service = new ToDoListItemService(dbContext, mapper, new NullLogger<ToDoListItemService>());

        var result = await service.GetItemsAsync(1);

        Assert.AreEqual(0, result.Count());

        var dummy = GetDummyToDoListItem();
        await service.AddItemAsync(dummyListId, dummy);

        result = await service.GetItemsAsync(1);
        Assert.AreEqual(1, result.Count());
    }
}