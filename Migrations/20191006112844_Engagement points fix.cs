using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Engagementpointsfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngagementPointsModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwardingUserId = table.Column<int>(nullable: true),
                    ReceivingUserId = table.Column<int>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    ReceivingDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngagementPointsModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngagementPointsModels_Users_AwardingUserId",
                        column: x => x.AwardingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementPointsModels_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngagementPointsModels_Users_ReceivingUserId",
                        column: x => x.ReceivingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngagementPointsModels_AwardingUserId",
                table: "EngagementPointsModels",
                column: "AwardingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementPointsModels_ProjectId",
                table: "EngagementPointsModels",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EngagementPointsModels_ReceivingUserId",
                table: "EngagementPointsModels",
                column: "ReceivingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngagementPointsModels");
        }
    }
}
