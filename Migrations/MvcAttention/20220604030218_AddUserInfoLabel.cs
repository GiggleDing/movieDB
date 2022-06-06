using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations.MvcAttention
{
    public partial class AddUserInfoLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label1",
                table: "UserInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label2",
                table: "UserInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label3",
                table: "UserInfo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label1",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Label2",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Label3",
                table: "UserInfo");
        }
    }
}
