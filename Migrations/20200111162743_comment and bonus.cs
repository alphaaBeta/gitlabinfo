using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class commentandbonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Groups_ParentGroupId",
                table: "ProjectRequests");

            migrationBuilder.AlterColumn<int>(
                name: "ParentGroupId",
                table: "ProjectRequests",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bonus",
                table: "EngagementPoints",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "EngagementPoints",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Groups_ParentGroupId",
                table: "ProjectRequests",
                column: "ParentGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Groups_ParentGroupId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "EngagementPoints");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "EngagementPoints");

            migrationBuilder.AlterColumn<int>(
                name: "ParentGroupId",
                table: "ProjectRequests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Groups_ParentGroupId",
                table: "ProjectRequests",
                column: "ParentGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
