namespace ToDoList.Models;

public record User(
    int Id,
    string Name,
    string? Url,
    string? AvatarUrl
);