using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoDAL.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "UserItem");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "UserItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserItem",
                nullable: true);
        }
    }
}
