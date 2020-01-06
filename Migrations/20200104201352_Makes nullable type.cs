using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Makesnullabletype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Surveys_SurveyId",
                table: "GroupOptions");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "GroupOptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Surveys_SurveyId",
                table: "GroupOptions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Surveys_SurveyId",
                table: "GroupOptions");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "GroupOptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Surveys_SurveyId",
                table: "GroupOptions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
