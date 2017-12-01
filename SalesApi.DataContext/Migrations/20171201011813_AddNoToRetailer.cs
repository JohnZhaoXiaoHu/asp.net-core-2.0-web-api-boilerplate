using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddNoToRetailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegacyCustomerId",
                table: "Retailer");

            migrationBuilder.AddColumn<string>(
                name: "No",
                table: "Retailer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                table: "Retailer");

            migrationBuilder.AddColumn<string>(
                name: "LegacyCustomerId",
                table: "Retailer",
                nullable: true);
        }
    }
}
