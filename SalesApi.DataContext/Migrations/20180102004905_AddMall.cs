using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddMall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MallDays",
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
                    table.PrimaryKey("PK_MallDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MallGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MallGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductForMalls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductForMalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForMalls_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MallCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MallGroupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    No = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Pinyin = table.Column<string>(maxLength: 50, nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    SubAreaId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MallCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MallCustomers_MallGroups_MallGroupId",
                        column: x => x.MallGroupId,
                        principalTable: "MallGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MallCustomers_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MallProductSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    EquivalentTon = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(maxLength: 5, nullable: true),
                    MallDayId = table.Column<int>(nullable: false),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    ProductForMallId = table.Column<int>(nullable: false),
                    ProductUnit = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    ShelfLife = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MallProductSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MallProductSnapshots_MallDays_MallDayId",
                        column: x => x.MallDayId,
                        principalTable: "MallDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MallProductSnapshots_ProductForMalls_ProductForMallId",
                        column: x => x.ProductForMallId,
                        principalTable: "ProductForMalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MallPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MallCustomerId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductForMallId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MallPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MallPrices_MallCustomers_MallCustomerId",
                        column: x => x.MallCustomerId,
                        principalTable: "MallCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MallPrices_ProductForMalls_ProductForMallId",
                        column: x => x.ProductForMallId,
                        principalTable: "ProductForMalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MallOrders",
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
                    MallCustomerId = table.Column<int>(nullable: false),
                    MallProductSnapshotId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Ordered = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MallOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MallOrders_MallCustomers_MallCustomerId",
                        column: x => x.MallCustomerId,
                        principalTable: "MallCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MallOrders_MallProductSnapshots_MallProductSnapshotId",
                        column: x => x.MallProductSnapshotId,
                        principalTable: "MallProductSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MallCustomers_MallGroupId",
                table: "MallCustomers",
                column: "MallGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MallCustomers_Name",
                table: "MallCustomers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallCustomers_No",
                table: "MallCustomers",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallCustomers_SubAreaId",
                table: "MallCustomers",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MallDays_Date",
                table: "MallDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallGroups_Name",
                table: "MallGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallOrders_MallCustomerId",
                table: "MallOrders",
                column: "MallCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MallOrders_MallProductSnapshotId",
                table: "MallOrders",
                column: "MallProductSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_MallOrders_Date_MallProductSnapshotId_MallCustomerId",
                table: "MallOrders",
                columns: new[] { "Date", "MallProductSnapshotId", "MallCustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallPrices_ProductForMallId",
                table: "MallPrices",
                column: "ProductForMallId");

            migrationBuilder.CreateIndex(
                name: "IX_MallPrices_MallCustomerId_ProductForMallId",
                table: "MallPrices",
                columns: new[] { "MallCustomerId", "ProductForMallId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MallProductSnapshots_MallDayId",
                table: "MallProductSnapshots",
                column: "MallDayId");

            migrationBuilder.CreateIndex(
                name: "IX_MallProductSnapshots_ProductForMallId",
                table: "MallProductSnapshots",
                column: "ProductForMallId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForMalls_ProductId",
                table: "ProductForMalls",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MallOrders");

            migrationBuilder.DropTable(
                name: "MallPrices");

            migrationBuilder.DropTable(
                name: "MallProductSnapshots");

            migrationBuilder.DropTable(
                name: "MallCustomers");

            migrationBuilder.DropTable(
                name: "MallDays");

            migrationBuilder.DropTable(
                name: "ProductForMalls");

            migrationBuilder.DropTable(
                name: "MallGroups");
        }
    }
}
