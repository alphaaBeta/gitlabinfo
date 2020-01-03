using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class RemoveUI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_UserId_SurveyId",
                table: "SurveyAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_UserId",
                table: "SurveyAnswers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyAnswers_UserId",
                table: "SurveyAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_UserId_SurveyId",
                table: "SurveyAnswers",
                columns: new[] { "UserId", "SurveyId" },
                unique: true);
        }
    }
}
