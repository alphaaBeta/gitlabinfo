using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Workdescriptionanduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "ReportedTimes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkDescriptions",
                columns: table => new
                {
                    WorkDescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDescriptions", x => x.WorkDescriptionId);
                    table.ForeignKey(
                        name: "FK_WorkDescriptions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkDescriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkDescriptionComments",
                columns: table => new
                {
                    WorkDescriptionCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommenterId = table.Column<int>(nullable: false),
                    WorkDescriptionId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDescriptionComments", x => x.WorkDescriptionCommentId);
                    table.ForeignKey(
                        name: "FK_WorkDescriptionComments_Users_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WorkDescriptionComments_WorkDescriptions_WorkDescriptionId",
                        column: x => x.WorkDescriptionId,
                        principalTable: "WorkDescriptions",
                        principalColumn: "WorkDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkDescriptionComments_CommenterId",
                table: "WorkDescriptionComments",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDescriptionComments_WorkDescriptionId",
                table: "WorkDescriptionComments",
                column: "WorkDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDescriptions_ProjectId",
                table: "WorkDescriptions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDescriptions_UserId",
                table: "WorkDescriptions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkDescriptionComments");

            migrationBuilder.DropTable(
                name: "WorkDescriptions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "ReportedTimes");
        }
    }
}
