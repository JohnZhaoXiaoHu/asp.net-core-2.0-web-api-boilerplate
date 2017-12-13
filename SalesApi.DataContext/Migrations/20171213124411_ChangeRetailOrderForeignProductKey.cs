using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class ChangeRetailOrderForeignProductKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailOrders_ProductForRetails_ProductForRetailId",
                table: "RetailOrders");

            migrationBuilder.RenameColumn(
                name: "ProductForRetailId",
                table: "RetailOrders",
                newName: "RetailProductSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_RetailOrders_Date_ProductForRetailId_RetailerId",
                table: "RetailOrders",
                newName: "IX_RetailOrders_Date_RetailProductSnapshotId_RetailerId");

            migrationBuilder.RenameIndex(
                name: "IX_RetailOrders_ProductForRetailId",
                table: "RetailOrders",
                newName: "IX_RetailOrders_RetailProductSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetailOrders_RetailProductSnapshots_RetailProductSnapshotId",
                table: "RetailOrders",
                column: "RetailProductSnapshotId",
                principalTable: "RetailProductSnapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailOrders_RetailProductSnapshots_RetailProductSnapshotId",
                table: "RetailOrders");

            migrationBuilder.RenameColumn(
                name: "RetailProductSnapshotId",
                table: "RetailOrders",
                newName: "ProductForRetailId");

            migrationBuilder.RenameIndex(
                name: "IX_RetailOrders_Date_RetailProductSnapshotId_RetailerId",
                table: "RetailOrders",
                newName: "IX_RetailOrders_Date_ProductForRetailId_RetailerId");

            migrationBuilder.RenameIndex(
                name: "IX_RetailOrders_RetailProductSnapshotId",
                table: "RetailOrders",
                newName: "IX_RetailOrders_ProductForRetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetailOrders_ProductForRetails_ProductForRetailId",
                table: "RetailOrders",
                column: "ProductForRetailId",
                principalTable: "ProductForRetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
