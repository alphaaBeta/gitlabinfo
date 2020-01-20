using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Groupoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Groups_AssignedGroupId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_AssignedGroupId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "AssignedGroupId",
                table: "Surveys");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WorkDescriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AnswerDate",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportedDate",
                table: "ReportedTimes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GroupOptions",
                columns: table => new
                {
                    GroupOptionsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    ReportTimeEnabled = table.Column<bool>(nullable: false),
                    EngagementPointsEnabled = table.Column<bool>(nullable: false),
                    WorkDescriptionEnabled = table.Column<bool>(nullable: false),
                    SurveyEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOptions", x => x.GroupOptionsId);
                    table.ForeignKey(
                        name: "FK_GroupOptions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOptions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptions_GroupId",
                table: "GroupOptions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptions_SurveyId",
                table: "GroupOptions",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupOptions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WorkDescriptions");

            migrationBuilder.DropColumn(
                name: "AnswerDate",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "ReportedDate",
                table: "ReportedTimes");

            migrationBuilder.AddColumn<int>(
                name: "AssignedGroupId",
                table: "Surveys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_AssignedGroupId",
                table: "Surveys",
                column: "AssignedGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Groups_AssignedGroupId",
                table: "Surveys",
                column: "AssignedGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
