using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class ChangeATableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrderBonuses_SubscriptionOrders_SubscriptionOrderId",
                table: "SubscriptionOrderBonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionOrderBonuses",
                table: "SubscriptionOrderBonuses");

            migrationBuilder.RenameTable(
                name: "SubscriptionOrderBonuses",
                newName: "SubscriptionOrderBonusDates");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonusDates",
                newName: "IX_SubscriptionOrderBonusDates_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonusDates",
                newName: "IX_SubscriptionOrderBonusDates_SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionOrderBonusDates",
                table: "SubscriptionOrderBonusDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrderBonusDates_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonusDates",
                column: "SubscriptionMonthPromotionBonusDateId",
                principalTable: "SubscriptionMonthPromotionBonusDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrderBonusDates_SubscriptionOrders_SubscriptionOrderId",
                table: "SubscriptionOrderBonusDates",
                column: "SubscriptionOrderId",
                principalTable: "SubscriptionOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrderBonusDates_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonusDates");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionOrderBonusDates_SubscriptionOrders_SubscriptionOrderId",
                table: "SubscriptionOrderBonusDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionOrderBonusDates",
                table: "SubscriptionOrderBonusDates");

            migrationBuilder.RenameTable(
                name: "SubscriptionOrderBonusDates",
                newName: "SubscriptionOrderBonuses");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionOrderBonusDates_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                newName: "IX_SubscriptionOrderBonuses_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionOrderBonusDates_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                newName: "IX_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionOrderBonuses",
                table: "SubscriptionOrderBonuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                column: "SubscriptionMonthPromotionBonusDateId",
                principalTable: "SubscriptionMonthPromotionBonusDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionOrderBonuses_SubscriptionOrders_SubscriptionOrderId",
                table: "SubscriptionOrderBonuses",
                column: "SubscriptionOrderId",
                principalTable: "SubscriptionOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
