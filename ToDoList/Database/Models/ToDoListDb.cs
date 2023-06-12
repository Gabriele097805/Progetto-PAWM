using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Database.Models
{
    [EntityTypeConfiguration(typeof(ToDoListDbEntityTypeConfiguration))]
    public class ToDoListDb
    {
        public required int Id { get; set; }
    
        public required int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<ToDoListItemDb>? Items { get; set; }
    }

    public class ToDoListDbEntityTypeConfiguration : IEntityTypeConfiguration<ToDoListDb>
    {
        public void Configure(EntityTypeBuilder<ToDoListDb> builder)
        {
            builder.ToTable("TODO_LIST");

            builder.HasKey(m => m.Id);
        }
    }
}