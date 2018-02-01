using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class UpdateSubscriptionDateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId_Date",
                table: "SubscriptionOrderDates");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId_Date",
                table: "SubscriptionMonthPromotionBonusDates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SubscriptionOrderDates",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SubscriptionMonthPromotionBonusDates",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId",
                table: "SubscriptionOrderDates",
                column: "SubscriptionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId",
                table: "SubscriptionMonthPromotionBonusDates",
                column: "SubscriptionMonthPromotionBonusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId",
                table: "SubscriptionOrderDates");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId",
                table: "SubscriptionMonthPromotionBonusDates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SubscriptionOrderDates",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SubscriptionMonthPromotionBonusDates",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId_Date",
                table: "SubscriptionOrderDates",
                columns: new[] { "SubscriptionOrderId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId_Date",
                table: "SubscriptionMonthPromotionBonusDates",
                columns: new[] { "SubscriptionMonthPromotionBonusId", "Date" },
                unique: true);
        }
    }
}
