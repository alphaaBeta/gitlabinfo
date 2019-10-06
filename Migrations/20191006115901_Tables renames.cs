using Microsoft.EntityFrameworkCore.Migrations;

namespace GitlabInfo.Migrations
{
    public partial class Tablesrenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPointsModels_Users_AwardingUserId",
                table: "EngagementPointsModels");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPointsModels_Projects_ProjectId",
                table: "EngagementPointsModels");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPointsModels_Users_ReceivingUserId",
                table: "EngagementPointsModels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupModel_Groups_GroupId",
                table: "UserGroupModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupModel_Users_UserId",
                table: "UserGroupModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupModel",
                table: "UserGroupModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EngagementPointsModels",
                table: "EngagementPointsModels");

            migrationBuilder.RenameTable(
                name: "UserGroupModel",
                newName: "UserGroups");

            migrationBuilder.RenameTable(
                name: "EngagementPointsModels",
                newName: "EngagementPoints");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupModel_GroupId",
                table: "UserGroups",
                newName: "IX_UserGroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPointsModels_ReceivingUserId",
                table: "EngagementPoints",
                newName: "IX_EngagementPoints_ReceivingUserId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPointsModels_ProjectId",
                table: "EngagementPoints",
                newName: "IX_EngagementPoints_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPointsModels_AwardingUserId",
                table: "EngagementPoints",
                newName: "IX_EngagementPoints_AwardingUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EngagementPoints",
                table: "EngagementPoints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPoints_Users_AwardingUserId",
                table: "EngagementPoints",
                column: "AwardingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPoints_Projects_ProjectId",
                table: "EngagementPoints",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPoints_Users_ReceivingUserId",
                table: "EngagementPoints",
                column: "ReceivingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPoints_Users_AwardingUserId",
                table: "EngagementPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPoints_Projects_ProjectId",
                table: "EngagementPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_EngagementPoints_Users_ReceivingUserId",
                table: "EngagementPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EngagementPoints",
                table: "EngagementPoints");

            migrationBuilder.RenameTable(
                name: "UserGroups",
                newName: "UserGroupModel");

            migrationBuilder.RenameTable(
                name: "EngagementPoints",
                newName: "EngagementPointsModels");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroupModel",
                newName: "IX_UserGroupModel_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPoints_ReceivingUserId",
                table: "EngagementPointsModels",
                newName: "IX_EngagementPointsModels_ReceivingUserId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPoints_ProjectId",
                table: "EngagementPointsModels",
                newName: "IX_EngagementPointsModels_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EngagementPoints_AwardingUserId",
                table: "EngagementPointsModels",
                newName: "IX_EngagementPointsModels_AwardingUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupModel",
                table: "UserGroupModel",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EngagementPointsModels",
                table: "EngagementPointsModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPointsModels_Users_AwardingUserId",
                table: "EngagementPointsModels",
                column: "AwardingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPointsModels_Projects_ProjectId",
                table: "EngagementPointsModels",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngagementPointsModels_Users_ReceivingUserId",
                table: "EngagementPointsModels",
                column: "ReceivingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupModel_Groups_GroupId",
                table: "UserGroupModel",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupModel_Users_UserId",
                table: "UserGroupModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
