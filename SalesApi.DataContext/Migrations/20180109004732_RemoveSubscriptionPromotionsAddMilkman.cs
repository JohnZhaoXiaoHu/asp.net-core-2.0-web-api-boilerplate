using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class RemoveSubscriptionPromotionsAddMilkman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPromotionEventBonus");

            migrationBuilder.DropTable(
                name: "SubscriptionPromotionSeriesBonus");

            migrationBuilder.DropTable(
                name: "SubscriptionPromotionEvent");

            migrationBuilder.DropTable(
                name: "SubscriptionPromotionSeries");

            migrationBuilder.CreateTable(
                name: "Milkmen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IdentityCardNo = table.Column<string>(maxLength: 50, nullable: true),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyId = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    No = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Pinyin = table.Column<string>(maxLength: 50, nullable: false),
                    SubAreaId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milkmen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milkmen_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Milkmen_SubAreaId",
                table: "Milkmen",
                column: "SubAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Milkmen");

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionSeries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    DateRepeatType = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionSeries_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    SubscriptionPromotionSeriesId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionEvent_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionEvent_SubscriptionPromotionSeries_SubscriptionPromotionSeriesId",
                        column: x => x.SubscriptionPromotionSeriesId,
                        principalTable: "SubscriptionPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionSeriesBonus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    SubscriptionPromotionSeriesId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionSeriesBonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionSeriesBonus_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionSeriesBonus_SubscriptionPromotionSeries_SubscriptionPromotionSeriesId",
                        column: x => x.SubscriptionPromotionSeriesId,
                        principalTable: "SubscriptionPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPromotionEventBonus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    SubscriptionPromotionEventId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPromotionEventBonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionEventBonus_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPromotionEventBonus_SubscriptionPromotionEvent_SubscriptionPromotionEventId",
                        column: x => x.SubscriptionPromotionEventId,
                        principalTable: "SubscriptionPromotionEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionEvent_SubscriptionPromotionSeriesId",
                table: "SubscriptionPromotionEvent",
                column: "SubscriptionPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionEvent_ProductForSubscriptionId_Date",
                table: "SubscriptionPromotionEvent",
                columns: new[] { "ProductForSubscriptionId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionEventBonus_ProductForSubscriptionId",
                table: "SubscriptionPromotionEventBonus",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionEventBonus_SubscriptionPromotionEventId",
                table: "SubscriptionPromotionEventBonus",
                column: "SubscriptionPromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionSeries_ProductForSubscriptionId",
                table: "SubscriptionPromotionSeries",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionSeriesBonus_ProductForSubscriptionId",
                table: "SubscriptionPromotionSeriesBonus",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPromotionSeriesBonus_SubscriptionPromotionSeriesId",
                table: "SubscriptionPromotionSeriesBonus",
                column: "SubscriptionPromotionSeriesId");
        }
    }
}
