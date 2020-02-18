using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoDAL.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "UserItem",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
