using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSomeUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId",
                table: "RetailPromotionEvents");

            migrationBuilder.DropIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId",
                table: "CountyPromotionGiftOrders");

            migrationBuilder.DropIndex(
                name: "IX_CountyPromotionEvents_ProductForCountyId",
                table: "CountyPromotionEvents");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId_Date",
                table: "RetailPromotionEvents",
                columns: new[] { "ProductForRetailId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders",
                columns: new[] { "CountyOrderId", "CountyPromotionEventBonusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEvents_ProductForCountyId_Date",
                table: "CountyPromotionEvents",
                columns: new[] { "ProductForCountyId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId_Date",
                table: "RetailPromotionEvents");

            migrationBuilder.DropIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders");

            migrationBuilder.DropIndex(
                name: "IX_CountyPromotionEvents_ProductForCountyId_Date",
                table: "CountyPromotionEvents");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId",
                table: "RetailPromotionEvents",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId",
                table: "CountyPromotionGiftOrders",
                column: "CountyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEvents_ProductForCountyId",
                table: "CountyPromotionEvents",
                column: "ProductForCountyId");
        }
    }
}
