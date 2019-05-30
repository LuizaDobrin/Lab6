using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAgendaProj.Migrations
{
    public partial class MakeUsernameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tasks_TaskId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comment",
                newName: "taskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TaskId",
                table: "Comment",
                newName: "IX_Comment_taskId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tasks_taskId",
                table: "Comment",
                column: "taskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tasks_taskId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "taskId",
                table: "Comment",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_taskId",
                table: "Comment",
                newName: "IX_Comment_TaskId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tasks_TaskId",
                table: "Comment",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
