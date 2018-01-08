using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductForSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxPrice = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    InternalPrice = table.Column<decimal>(nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductForSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForSubscriptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 10, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Initialized = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionProductSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    BoxPrice = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    EquivalentTon = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    InternalPrice = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(maxLength: 5, nullable: true),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    ProductUnit = table.Column<int>(nullable: false),
                    ShelfLife = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(maxLength: 50, nullable: false),
                    SubscriptionDayId = table.Column<int>(nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionProductSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionProductSnapshots_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionProductSnapshots_SubscriptionDays_SubscriptionDayId",
                        column: x => x.SubscriptionDayId,
                        principalTable: "SubscriptionDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductForSubscriptions_ProductId",
                table: "ProductForSubscriptions",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDays_Date",
                table: "SubscriptionDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionProductSnapshots_ProductForSubscriptionId",
                table: "SubscriptionProductSnapshots",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionProductSnapshots_SubscriptionDayId",
                table: "SubscriptionProductSnapshots",
                column: "SubscriptionDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionProductSnapshots");

            migrationBuilder.DropTable(
                name: "ProductForSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionDays");
        }
    }
}
