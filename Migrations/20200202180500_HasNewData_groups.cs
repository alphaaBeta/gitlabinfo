using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class HasNewData_groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNewData",
                table: "GroupOptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNewData",
                table: "GroupOptions");
        }
    }
}
