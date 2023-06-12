using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class ToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoListToItem",
                table: "TODO_LIST_ITEM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TODO_LIST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO_LIST", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TODO_LIST_ITEM_ToDoListToItem",
                table: "TODO_LIST_ITEM",
                column: "ToDoListToItem");

            migrationBuilder.AddForeignKey(
                name: "FK_TODO_LIST_ITEM_TODO_LIST_ToDoListToItem",
                table: "TODO_LIST_ITEM",
                column: "ToDoListToItem",
                principalTable: "TODO_LIST",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TODO_LIST_ITEM_TODO_LIST_ToDoListToItem",
                table: "TODO_LIST_ITEM");

            migrationBuilder.DropTable(
                name: "TODO_LIST");

            migrationBuilder.DropIndex(
                name: "IX_TODO_LIST_ITEM_ToDoListToItem",
                table: "TODO_LIST_ITEM");

            migrationBuilder.DropColumn(
                name: "ToDoListToItem",
                table: "TODO_LIST_ITEM");
        }
    }
}
