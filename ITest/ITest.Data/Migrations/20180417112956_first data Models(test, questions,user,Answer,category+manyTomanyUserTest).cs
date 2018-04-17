using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITest.Data.Migrations
{
    public partial class firstdataModelstestquestionsuserAnswercategorymanyTomanyUserTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId1",
                table: "UserTests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId2",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_UserId1",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_UserId2",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "UserTests");

            migrationBuilder.AddColumn<bool>(
                name: "PassedTest",
                table: "UserTests",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassedTest",
                table: "UserTests");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "UserTests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId1",
                table: "UserTests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId2",
                table: "UserTests",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId1",
                table: "UserTests",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId2",
                table: "UserTests",
                column: "UserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
