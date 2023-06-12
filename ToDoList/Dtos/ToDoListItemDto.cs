namespace ToDoList.Dtos;

public record ToDoListItemDto(
   int Id,
   bool Checked,
   string Text
);
