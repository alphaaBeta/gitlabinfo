using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class testowa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Users_RequesteeId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProjectRequests_ProjectRequestModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectRequestModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_RequesteeId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "ProjectRequestModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RequesteeId",
                table: "ProjectRequests");

            migrationBuilder.CreateTable(
                name: "UserProjectRequests",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ProjectRequestId = table.Column<int>(nullable: false),
                    IsRequestee = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectRequests", x => new { x.UserId, x.ProjectRequestId });
                    table.ForeignKey(
                        name: "FK_UserProjectRequests_ProjectRequests_ProjectRequestId",
                        column: x => x.ProjectRequestId,
                        principalTable: "ProjectRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectRequests_ProjectRequestId",
                table: "UserProjectRequests",
                column: "ProjectRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectRequests");

            migrationBuilder.AddColumn<int>(
                name: "ProjectRequestModelId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequesteeId",
                table: "ProjectRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectRequestModelId",
                table: "Users",
                column: "ProjectRequestModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_RequesteeId",
                table: "ProjectRequests",
                column: "RequesteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Users_RequesteeId",
                table: "ProjectRequests",
                column: "RequesteeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProjectRequests_ProjectRequestModelId",
                table: "Users",
                column: "ProjectRequestModelId",
                principalTable: "ProjectRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
