using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectRequestModelId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequesteeId = table.Column<int>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    ParentGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRequests_Groups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectRequests_Users_RequesteeId",
                        column: x => x.RequesteeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectRequestModelId",
                table: "Users",
                column: "ProjectRequestModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_ParentGroupId",
                table: "ProjectRequests",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_RequesteeId",
                table: "ProjectRequests",
                column: "RequesteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProjectRequests_ProjectRequestModelId",
                table: "Users",
                column: "ProjectRequestModelId",
                principalTable: "ProjectRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProjectRequests_ProjectRequestModelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectRequestModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectRequestModelId",
                table: "Users");
        }
    }
}
