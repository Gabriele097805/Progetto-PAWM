using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ToDoList.Database;
using ToDoList.Mappings;

namespace ToDoListTest.Mocks;

/// <summary>
/// Extension and helper methods to handle mocks.
/// </summary>
public static class MocksExtensions
{
    public static IMapper AddMapper()
    {
        var mapper = new MapperConfiguration(config => 
        {
            config.AddProfile(new DbProfile());
        });

        return mapper.CreateMapper();
    }

    public static TestDbContext GetInMemoryDbContext()
    {
        var dbLogger = new NullLogger<TestDbContext>();

        var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ToDoListDatabase")
            .Options;
    
        return new TestDbContext(dbContextOptions, dbLogger);
    }
}
