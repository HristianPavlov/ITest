using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITest.Data.Migrations
{
    public partial class removingserializerfromuserTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedAnswers",
                table: "UserTests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerializedAnswers",
                table: "UserTests",
                nullable: true);
        }
    }
}
