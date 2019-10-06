using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Another : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_GroupModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedTimes_Groups_GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.DropIndex(
                name: "IX_ReportedTimes_GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_GroupModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "AssignedGroupId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AssignedGroupId",
                table: "Projects",
                column: "AssignedGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_AssignedGroupId",
                table: "Projects",
                column: "AssignedGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_AssignedGroupId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AssignedGroupId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AssignedGroupId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "GroupModelId",
                table: "ReportedTimes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupModelId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReportedTimes_GroupModelId",
                table: "ReportedTimes",
                column: "GroupModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GroupModelId",
                table: "Projects",
                column: "GroupModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_GroupModelId",
                table: "Projects",
                column: "GroupModelId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedTimes_Groups_GroupModelId",
                table: "ReportedTimes",
                column: "GroupModelId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
