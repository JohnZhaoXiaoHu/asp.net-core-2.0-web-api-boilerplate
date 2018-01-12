using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSubscriptionPromotionDeliveryDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionMonthBonusDeliveryDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DayBonusCount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionPromotionMonthBonusId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionMonthBonusDeliveryDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionMonthBonusDeliveryDates_SubscriptionPromotionMonthBonuses_SubscriptionPromotionMonthBonusId",
                        column: x => x.SubscriptionPromotionMonthBonusId,
                        principalTable: "SubscriptionPromotionMonthBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionMonthBonusDeliveryDates_SubscriptionPromotionMonthBonusId_Date",
                table: "SubscriptionPromotionMonthBonusDeliveryDates",
                columns: new[] { "SubscriptionPromotionMonthBonusId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPromotionMonthBonusDeliveryDates");
        }
    }
}
