using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class RemoveSalesType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "RetailPromotionSeries");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "RetailProductSnapshots");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "Retailers");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "ProductForRetails");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "ProductForMalls");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "ProductForCounties");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "ProductForCollectives");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "MallProductSnapshots");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "MallCustomers");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CountyPromotionSeries");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CountyProductSnapshots");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CountyOrders");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CountyAgents");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CollectiveProductSnapshots");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CollectiveCustomers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "RetailPromotionSeries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "RetailProductSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "Retailers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "ProductForRetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "ProductForMalls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "ProductForCounties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "ProductForCollectives",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "MallProductSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "MallCustomers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CountyPromotionSeries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CountyProductSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CountyOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CountyAgents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CollectiveProductSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CollectiveCustomers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
