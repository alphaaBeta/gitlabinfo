using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Engagementpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_GroupModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedTimes_Groups_GroupId",
                table: "ReportedTimes");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ReportedTimes",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedTimes_GroupId",
                table: "ReportedTimes",
                newName: "IX_ReportedTimes_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReportedTimes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupModelId",
                table: "ReportedTimes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupModelId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportedTimes_GroupModelId",
                table: "ReportedTimes",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedTimes_Projects_ProjectId2",
                table: "ReportedTimes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_GroupModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedTimes_Groups_GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportedTimes_Projects_ProjectId2",
                table: "ReportedTimes");

            migrationBuilder.DropIndex(
                name: "IX_ReportedTimes_GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ReportedTimes");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "ReportedTimes");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ReportedTimes",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportedTimes_ProjectId",
                table: "ReportedTimes",
                newName: "IX_ReportedTimes_GroupId");

            migrationBuilder.AlterColumn<int>(
                name: "GroupModelId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_GroupModelId",
                table: "Projects",
                column: "GroupModelId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportedTimes_Groups_GroupId",
                table: "ReportedTimes",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
