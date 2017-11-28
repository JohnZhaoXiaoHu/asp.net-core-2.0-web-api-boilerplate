using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddProductAndRetailProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    EquivalentTon = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductUnit = table.Column<int>(type: "int", nullable: false),
                    ShelfLife = table.Column<int>(type: "int", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductForRetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxPrice = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    EquivalentBox = table.Column<int>(type: "int", nullable: false),
                    InternalPrice = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    IsOrderByBox = table.Column<bool>(type: "bit", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinOrderCount = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    OrderDivisor = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SalesType = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductForRetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForRetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductForRetails_ProductId",
                table: "ProductForRetails",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductForRetails");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
