using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITest.Data.Migrations
{
    public partial class FixingUserTeststable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassedTest",
                table: "UserTests",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserTests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserTests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserTests",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "UserTests",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SerializedAnswers",
                table: "UserTests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "SerializedAnswers",
                table: "UserTests");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "UserTests",
                newName: "PassedTest");
        }
    }
}
