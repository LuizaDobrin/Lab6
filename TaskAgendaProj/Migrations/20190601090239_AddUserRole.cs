using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAgendaProj.Migrations
{
    public partial class AddUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tasks_taskId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "taskId",
                table: "Comment",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_taskId",
                table: "Comment",
                newName: "IX_Comment_TaskId");

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tasks_TaskId",
                table: "Comment",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tasks_TaskId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comment",
                newName: "taskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TaskId",
                table: "Comment",
                newName: "IX_Comment_taskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tasks_taskId",
                table: "Comment",
                column: "taskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
