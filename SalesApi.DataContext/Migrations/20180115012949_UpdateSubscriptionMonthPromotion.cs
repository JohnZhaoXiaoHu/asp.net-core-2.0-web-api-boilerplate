using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class UpdateSubscriptionMonthPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                newName: "PresetDayBonusCount");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_DayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                newName: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_PresetDayBonusCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PresetDayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                newName: "DayBonusCount");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_PresetDayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                newName: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_DayBonusCount");
        }
    }
}
