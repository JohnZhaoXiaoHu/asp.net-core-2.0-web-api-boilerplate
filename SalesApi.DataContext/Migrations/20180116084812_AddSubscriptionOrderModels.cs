using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSubscriptionOrderModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MilkmanId = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PresetDayBonus = table.Column<int>(nullable: false),
                    PresetDayCount = table.Column<int>(nullable: false),
                    PresetDayGift = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionId = table.Column<int>(nullable: true),
                    SubscriptionProductSnapshotId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_Milkmen_MilkmanId",
                        column: x => x.MilkmanId,
                        principalTable: "Milkmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_SubscriptionMonthPromotions_SubscriptionMonthPromotionId",
                        column: x => x.SubscriptionMonthPromotionId,
                        principalTable: "SubscriptionMonthPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_SubscriptionProductSnapshots_SubscriptionProductSnapshotId",
                        column: x => x.SubscriptionProductSnapshotId,
                        principalTable: "SubscriptionProductSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionOrderBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionBonusDateId = table.Column<int>(nullable: false),
                    SubscriptionOrderId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrderBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                        column: x => x.SubscriptionMonthPromotionBonusDateId,
                        principalTable: "SubscriptionMonthPromotionBonusDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderBonuses_SubscriptionOrders_SubscriptionOrderId",
                        column: x => x.SubscriptionOrderId,
                        principalTable: "SubscriptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionOrderDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionOrderId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrderDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderDates_SubscriptionOrders_SubscriptionOrderId",
                        column: x => x.SubscriptionOrderId,
                        principalTable: "SubscriptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                column: "SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                columns: new[] { "SubscriptionOrderId", "SubscriptionMonthPromotionBonusDateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId_Date",
                table: "SubscriptionOrderDates",
                columns: new[] { "SubscriptionOrderId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_MilkmanId",
                table: "SubscriptionOrders",
                column: "MilkmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_SubscriptionMonthPromotionId",
                table: "SubscriptionOrders",
                column: "SubscriptionMonthPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_SubscriptionProductSnapshotId",
                table: "SubscriptionOrders",
                column: "SubscriptionProductSnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionOrderBonuses");

            migrationBuilder.DropTable(
                name: "SubscriptionOrderDates");

            migrationBuilder.DropTable(
                name: "SubscriptionOrders");
        }
    }
}
