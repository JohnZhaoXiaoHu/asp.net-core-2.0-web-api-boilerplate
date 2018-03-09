using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddIsInternalToSubscriptionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInternal",
                table: "SubscriptionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotionBonuses",
                column: "ProductForSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionMonthPromotionBonuses_ProductForSubscriptions_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotionBonuses",
                column: "ProductForSubscriptionId",
                principalTable: "ProductForSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionMonthPromotionBonuses_ProductForSubscriptions_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotionBonuses");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotionBonuses");

            migrationBuilder.DropColumn(
                name: "IsInternal",
                table: "SubscriptionOrders");
        }
    }
}
