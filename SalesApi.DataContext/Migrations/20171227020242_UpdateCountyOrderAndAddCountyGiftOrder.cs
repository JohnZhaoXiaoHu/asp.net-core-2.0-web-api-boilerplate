using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class UpdateCountyOrderAndAddCountyGiftOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionGift",
                table: "RetailOrders");

            migrationBuilder.DropColumn(
                name: "PromotionGift",
                table: "CountyOrders");

            migrationBuilder.AddColumn<int>(
                name: "SalesType",
                table: "CountyOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountyPromotionGiftOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountyOrderId = table.Column<int>(nullable: false),
                    CountyPromotionEventBonusId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PromotionGift = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyPromotionGiftOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionGiftOrders_CountyOrders_CountyOrderId",
                        column: x => x.CountyOrderId,
                        principalTable: "CountyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyPromotionGiftOrders_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                        column: x => x.CountyPromotionEventBonusId,
                        principalTable: "CountyPromotionEventBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId",
                table: "CountyPromotionGiftOrders",
                column: "CountyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrders_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders",
                column: "CountyPromotionEventBonusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountyPromotionGiftOrders");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "CountyOrders");

            migrationBuilder.AddColumn<int>(
                name: "PromotionGift",
                table: "RetailOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromotionGift",
                table: "CountyOrders",
                nullable: false,
                defaultValue: 0);
        }
    }
}
