using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class groupoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WorkDescriptionCommentsEnabled",
                table: "GroupOptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDescriptionCommentsEnabled",
                table: "GroupOptions");
        }
    }
}
