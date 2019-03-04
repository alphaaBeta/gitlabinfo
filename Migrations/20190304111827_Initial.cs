using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GitLabId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GitLabId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    GitLabId = table.Column<int>(nullable: false),
                    FirstJoined = table.Column<DateTime>(nullable: false),
                    LastJoined = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.GitLabId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    GitLabId = table.Column<int>(nullable: false),
                    GroupGitLabId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.GitLabId);
                    table.ForeignKey(
                        name: "FK_Projects_Groups_GroupGitLabId",
                        column: x => x.GroupGitLabId,
                        principalTable: "Groups",
                        principalColumn: "GitLabId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGitLabId = table.Column<int>(nullable: false),
                    GroupGitLabId = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserGitLabId, x.GroupGitLabId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupGitLabId",
                        column: x => x.GroupGitLabId,
                        principalTable: "Groups",
                        principalColumn: "GitLabId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Users_UserGitLabId",
                        column: x => x.UserGitLabId,
                        principalTable: "Users",
                        principalColumn: "GitLabId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GroupGitLabId",
                table: "Projects",
                column: "GroupGitLabId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupGitLabId",
                table: "UserGroup",
                column: "GroupGitLabId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
