using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSubscriptionPromotionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPromotions",
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
                    MonthSpan = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    StartDate = table.Column<int>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotions_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionMonths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionPromotionId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionMonths_SubscriptionPromotions_SubscriptionPromotionId",
                        column: x => x.SubscriptionPromotionId,
                        principalTable: "SubscriptionPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionMonthBonuses",
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
                    SubscriptionPromotionMonthId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionMonthBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionMonthBonuses_SubscriptionPromotionMonths_SubscriptionPromotionMonthId",
                        column: x => x.SubscriptionPromotionMonthId,
                        principalTable: "SubscriptionPromotionMonths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionMonthBonuses_SubscriptionPromotionMonthId_ProductForSubscriptionId_DayBonusCount",
                table: "SubscriptionPromotionMonthBonuses",
                columns: new[] { "SubscriptionPromotionMonthId", "ProductForSubscriptionId", "DayBonusCount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionMonths_SubscriptionPromotionId_Year_Month",
                table: "SubscriptionPromotionMonths",
                columns: new[] { "SubscriptionPromotionId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotions_ProductForSubscriptionId",
                table: "SubscriptionPromotions",
                column: "ProductForSubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPromotionMonthBonuses");

            migrationBuilder.DropTable(
                name: "SubscriptionPromotionMonths");

            migrationBuilder.DropTable(
                name: "SubscriptionPromotions");
        }
    }
}
