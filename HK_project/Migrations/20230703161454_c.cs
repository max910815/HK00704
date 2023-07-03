using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HK_project.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIFiles_Applications_ApplicationId",
                table: "AIFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Member_MemberId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Applications_ApplicationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_MemberId",
                table: "Application",
                newName: "IX_Application_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplicationId");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "ChatId",
                keyValue: "C0001",
                column: "ChatTime",
                value: new DateTime(2023, 7, 4, 0, 14, 54, 370, DateTimeKind.Local).AddTicks(6331));

            migrationBuilder.AddForeignKey(
                name: "FK_AIFiles_Application_ApplicationId",
                table: "AIFiles",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Member_MemberId",
                table: "Application",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Application_ApplicationId",
                table: "Users",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIFiles_Application_ApplicationId",
                table: "AIFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Member_MemberId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Application_ApplicationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_Application_MemberId",
                table: "Applications",
                newName: "IX_Applications_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "ApplicationId");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "ChatId",
                keyValue: "C0001",
                column: "ChatTime",
                value: new DateTime(2023, 7, 3, 17, 28, 11, 712, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.AddForeignKey(
                name: "FK_AIFiles_Applications_ApplicationId",
                table: "AIFiles",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Member_MemberId",
                table: "Applications",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Applications_ApplicationId",
                table: "Users",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
