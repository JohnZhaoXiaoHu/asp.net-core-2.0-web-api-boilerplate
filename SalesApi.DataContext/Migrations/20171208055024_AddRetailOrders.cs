using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddRetailOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailProductSnapshots_ProductForRetails_ProductForRetailId",
                table: "RetailProductSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailProductSnapshots_RetailDays_RetailDayId",
                table: "RetailProductSnapshots");

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "SalesDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "SalesDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "SalesDays",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "SalesDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "RetailProductSnapshots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "RetailProductSnapshots",
                type: "decimal(7, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "RetailProductSnapshots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RetailProductSnapshots",
                type: "decimal(10, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RetailProductSnapshots",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegacyProductId",
                table: "RetailProductSnapshots",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "RetailProductSnapshots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InternalPrice",
                table: "RetailProductSnapshots",
                type: "decimal(10, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "RetailProductSnapshots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EquivalentTon",
                table: "RetailProductSnapshots",
                type: "decimal(7, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "RetailProductSnapshots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BoxPrice",
                table: "RetailProductSnapshots",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "RetailProductSnapshots",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "RetailDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "RetailDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "RetailDays",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "RetailDays",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RetailOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 10, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Gift = table.Column<int>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyOrderId = table.Column<string>(maxLength: 20, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ordered = table.Column<int>(nullable: false),
                    ProductForRetailId = table.Column<int>(nullable: false),
                    RetailPromotionEventId = table.Column<int>(nullable: true),
                    RetailPromotionEventId1 = table.Column<int>(nullable: true),
                    RetailerId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailOrders_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId",
                        column: x => x.RetailPromotionEventId,
                        principalTable: "RetailPromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId1",
                        column: x => x.RetailPromotionEventId1,
                        principalTable: "RetailPromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailOrders_Retailers_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesDays_Date",
                table: "SalesDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailDays_Date",
                table: "RetailDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_ProductForRetailId",
                table: "RetailOrders",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_RetailPromotionEventId",
                table: "RetailOrders",
                column: "RetailPromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_RetailPromotionEventId1",
                table: "RetailOrders",
                column: "RetailPromotionEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_RetailerId",
                table: "RetailOrders",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_Date_ProductForRetailId_RetailerId",
                table: "RetailOrders",
                columns: new[] { "Date", "ProductForRetailId", "RetailerId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailProductSnapshots_ProductForRetails_ProductForRetailId",
                table: "RetailProductSnapshots",
                column: "ProductForRetailId",
                principalTable: "ProductForRetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailProductSnapshots_RetailDays_RetailDayId",
                table: "RetailProductSnapshots",
                column: "RetailDayId",
                principalTable: "RetailDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailProductSnapshots_ProductForRetails_ProductForRetailId",
                table: "RetailProductSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailProductSnapshots_RetailDays_RetailDayId",
                table: "RetailProductSnapshots");

            migrationBuilder.DropTable(
                name: "RetailOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesDays_Date",
                table: "SalesDays");

            migrationBuilder.DropIndex(
                name: "IX_RetailDays_Date",
                table: "RetailDays");

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "SalesDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "SalesDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "SalesDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "SalesDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "RetailProductSnapshots",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 6)");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RetailProductSnapshots",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 6)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "LegacyProductId",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "InternalPrice",
                table: "RetailProductSnapshots",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 6)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "EquivalentTon",
                table: "RetailProductSnapshots",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 6)");

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "BoxPrice",
                table: "RetailProductSnapshots",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "RetailProductSnapshots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "RetailDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastAction",
                table: "RetailDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "RetailDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "CreateUser",
                table: "RetailDays",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailProductSnapshots_ProductForRetails_ProductForRetailId",
                table: "RetailProductSnapshots",
                column: "ProductForRetailId",
                principalTable: "ProductForRetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailProductSnapshots_RetailDays_RetailDayId",
                table: "RetailProductSnapshots",
                column: "RetailDayId",
                principalTable: "RetailDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
