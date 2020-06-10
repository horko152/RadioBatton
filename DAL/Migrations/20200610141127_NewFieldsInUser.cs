using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class NewFieldsInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userinfo",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "userinfo",
                table: "users");
        }
    }
}
