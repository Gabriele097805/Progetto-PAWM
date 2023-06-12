using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Models;

[EntityTypeConfiguration(typeof(ToDoListItemDbEntityTypeConfiguration))]
public class ToDoListItemDb
{
    public required int Id { get; set; }

    public required ToDoListDb List { get; set; }

    public string Text { get; set; } = string.Empty;

    public bool Checked { get; set; } = false;
}

public class ToDoListItemDbEntityTypeConfiguration : IEntityTypeConfiguration<ToDoListItemDb>
{
    public void Configure(EntityTypeBuilder<ToDoListItemDb> builder)
    {
        builder.ToTable("TODO_LIST_ITEM");

        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.List)
            .WithMany(l => l.Items)
            .HasForeignKey("ToDoListToItem");
    }
}