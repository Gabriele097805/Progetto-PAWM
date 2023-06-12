using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ToDoList.Controllers;
using ToDoList.Models;
using ToDoList.Services;
using ToDoListTest.Mocks;

namespace ToDoListTest.Controllers;

[TestClass]
public class ToDoListItemsControllerTest
{
    private static ToDoListItem GetDummyToDoListItem() => new(1, 1, true, "text");

    [TestMethod]
    public async Task GetItemsAsync()
    {
        var dummyItem = GetDummyToDoListItem();
        var service = new Mock<IToDoListItemService>();
        service.Setup(s => s.GetItemsAsync(1))
            .ReturnsAsync(new [] { dummyItem });
        var mapper = MocksExtensions.AddMapper();

        var controller = new ToDoListItemsController(service.Object, mapper, new NullLogger<ToDoListItemsController>());
        var result = await controller.GetItems(1);

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(dummyItem.Id, result.First().Id);
        Assert.AreEqual(dummyItem.Checked, result.First().Checked);
        Assert.AreEqual(dummyItem.Text, result.First().Text);
    }
}