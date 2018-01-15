using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddNewSubscriptionPromotionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionMonthPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    DayBase = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EndDate = table.Column<int>(nullable: true),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    PromotionType = table.Column<int>(nullable: false),
                    StartDate = table.Column<int>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotions_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionMonthPromotionBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    DayBonusCount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotionBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotions_SubscriptionMonthPromotionId",
                        column: x => x.SubscriptionMonthPromotionId,
                        principalTable: "SubscriptionMonthPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionMonthPromotionBonusDates",
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
                    SubscriptionMonthPromotionBonusId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotionBonusDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionBonusId",
                        column: x => x.SubscriptionMonthPromotionBonusId,
                        principalTable: "SubscriptionMonthPromotionBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId_Date",
                table: "SubscriptionMonthPromotionBonusDates",
                columns: new[] { "SubscriptionMonthPromotionBonusId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_DayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                columns: new[] { "SubscriptionMonthPromotionId", "ProductForSubscriptionId", "DayBonusCount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotions_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotions",
                column: "ProductForSubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotionBonusDates");

            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotionBonuses");

            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotions");
        }
    }
}
