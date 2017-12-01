using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddDateRepeatToRetailPromotionSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateRepeatType",
                table: "RetailPromotionSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Step",
                table: "RetailPromotionSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRepeatType",
                table: "RetailPromotionSeries");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "RetailPromotionSeries");
        }
    }
}
