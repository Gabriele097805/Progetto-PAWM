using AutoMapper;
using ToDoList.Database.Models;
using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.Mappings;

public class DbProfile : Profile
{
    public DbProfile()
    {
        CreateMap<ToDoListItemDb, ToDoListItem>();
        CreateMap<ToDoListItem, ToDoListItemDb>();

        CreateMap<ToDoListItem, ToDoListItemDto>();
        CreateMap<ToDoListItemDto, ToDoListItem>();

        CreateMap<Models.ToDoList, ToDoListDb>();
        CreateMap<ToDoListDb, Models.ToDoList>();

        CreateMap<Models.ToDoList, ToDoListDto>();
        CreateMap<ToDoListDto, Models.ToDoList>();
    }
}