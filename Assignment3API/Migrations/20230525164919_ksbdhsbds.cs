using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3API.Migrations
{
    public partial class ksbdhsbds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
