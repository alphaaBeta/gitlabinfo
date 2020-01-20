using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class ProjectIDtosurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "SurveyAnswers",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_ProjectId",
                table: "SurveyAnswers",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswers_Projects_ProjectId",
                table: "SurveyAnswers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_Projects_ProjectId",
                table: "SurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_ProjectId",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "SurveyAnswers");
        }
    }
}
