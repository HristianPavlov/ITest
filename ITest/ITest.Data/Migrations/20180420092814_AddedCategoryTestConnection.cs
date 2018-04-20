using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITest.Data.Migrations
{
    public partial class AddedCategoryTestConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Test",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Question",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Test_CategoryId",
                table: "Test",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Categories_CategoryId",
                table: "Test",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Categories_CategoryId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_CategoryId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Test");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Question",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
