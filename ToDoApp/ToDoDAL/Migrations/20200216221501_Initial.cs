using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoDAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoListItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    TimeCreated = table.Column<DateTime>(nullable: false),
                    UserItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoListItem_UserItem_UserItemId",
                        column: x => x.UserItemId,
                        principalTable: "UserItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ToDoListItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItem_ToDoListItem_ToDoListItemId",
                        column: x => x.ToDoListItemId,
                        principalTable: "ToDoListItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToDoListItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserItemId = table.Column<int>(nullable: false),
                    ToDoListItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToDoListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToDoListItem_ToDoListItem_ToDoListItemId",
                        column: x => x.ToDoListItemId,
                        principalTable: "ToDoListItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToDoListItem_UserItem_UserItemId",
                        column: x => x.UserItemId,
                        principalTable: "UserItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_ToDoListItemId",
                table: "ToDoItem",
                column: "ToDoListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoListItem_UserItemId",
                table: "ToDoListItem",
                column: "UserItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToDoListItem_ToDoListItemId",
                table: "UserToDoListItem",
                column: "ToDoListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToDoListItem_UserItemId",
                table: "UserToDoListItem",
                column: "UserItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleItem");

            migrationBuilder.DropTable(
                name: "ToDoItem");

            migrationBuilder.DropTable(
                name: "UserToDoListItem");

            migrationBuilder.DropTable(
                name: "ToDoListItem");

            migrationBuilder.DropTable(
                name: "UserItem");
        }
    }
}
