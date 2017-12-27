using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddRetailPromotionGiftOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountyPromotionGiftOrders_CountyOrders_CountyOrderId",
                table: "CountyPromotionGiftOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CountyPromotionGiftOrders_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountyPromotionGiftOrders",
                table: "CountyPromotionGiftOrders");

            migrationBuilder.RenameTable(
                name: "CountyPromotionGiftOrders",
                newName: "CountyPromotionGiftOrder");

            migrationBuilder.RenameIndex(
                name: "IX_CountyPromotionGiftOrders_CountyOrderId_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder",
                newName: "IX_CountyPromotionGiftOrder_CountyOrderId_CountyPromotionEventBonusId");

            migrationBuilder.RenameIndex(
                name: "IX_CountyPromotionGiftOrders_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder",
                newName: "IX_CountyPromotionGiftOrder_CountyPromotionEventBonusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountyPromotionGiftOrder",
                table: "CountyPromotionGiftOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RetailPromotionGiftOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PromotionGift = table.Column<int>(nullable: false),
                    RetailOrderId = table.Column<int>(nullable: false),
                    RetailPromotionEventBonusId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPromotionGiftOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPromotionGiftOrder_RetailOrders_RetailOrderId",
                        column: x => x.RetailOrderId,
                        principalTable: "RetailOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailPromotionGiftOrder_RetailPromotionEventBonuses_RetailPromotionEventBonusId",
                        column: x => x.RetailPromotionEventBonusId,
                        principalTable: "RetailPromotionEventBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionGiftOrder_RetailPromotionEventBonusId",
                table: "RetailPromotionGiftOrder",
                column: "RetailPromotionEventBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionGiftOrder_RetailOrderId_RetailPromotionEventBonusId",
                table: "RetailPromotionGiftOrder",
                columns: new[] { "RetailOrderId", "RetailPromotionEventBonusId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CountyPromotionGiftOrder_CountyOrders_CountyOrderId",
                table: "CountyPromotionGiftOrder",
                column: "CountyOrderId",
                principalTable: "CountyOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountyPromotionGiftOrder_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder",
                column: "CountyPromotionEventBonusId",
                principalTable: "CountyPromotionEventBonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountyPromotionGiftOrder_CountyOrders_CountyOrderId",
                table: "CountyPromotionGiftOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_CountyPromotionGiftOrder_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder");

            migrationBuilder.DropTable(
                name: "RetailPromotionGiftOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountyPromotionGiftOrder",
                table: "CountyPromotionGiftOrder");

            migrationBuilder.RenameTable(
                name: "CountyPromotionGiftOrder",
                newName: "CountyPromotionGiftOrders");

            migrationBuilder.RenameIndex(
                name: "IX_CountyPromotionGiftOrder_CountyOrderId_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders",
                newName: "IX_CountyPromotionGiftOrders_CountyOrderId_CountyPromotionEventBonusId");

            migrationBuilder.RenameIndex(
                name: "IX_CountyPromotionGiftOrder_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders",
                newName: "IX_CountyPromotionGiftOrders_CountyPromotionEventBonusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountyPromotionGiftOrders",
                table: "CountyPromotionGiftOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountyPromotionGiftOrders_CountyOrders_CountyOrderId",
                table: "CountyPromotionGiftOrders",
                column: "CountyOrderId",
                principalTable: "CountyOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountyPromotionGiftOrders_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrders",
                column: "CountyPromotionEventBonusId",
                principalTable: "CountyPromotionEventBonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
