namespace ToDoList.Models;

public record ToDoListItem(
   int Id,
   int ListId,
   bool Checked,
   string Text
);
