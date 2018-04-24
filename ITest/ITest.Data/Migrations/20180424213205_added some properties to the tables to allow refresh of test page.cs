using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITest.Data.Migrations
{
    public partial class addedsomepropertiestothetablestoallowrefreshoftestpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "UserTests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeInMinutes",
                table: "Test",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "TimeInMinutes",
                table: "Test");
        }
    }
}
