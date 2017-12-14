using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddCollectiveModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectiveCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_CollectiveCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectiveCustomers_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectiveDays",
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
                    table.PrimaryKey("PK_CollectiveDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductForCollectives",
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
                    table.PrimaryKey("PK_ProductForCollectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForCollectives_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectiveProductSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    CollectiveDayId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    EquivalentTon = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(maxLength: 5, nullable: true),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    ProductForCollectiveId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_CollectiveProductSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectiveProductSnapshots_CollectiveDays_CollectiveDayId",
                        column: x => x.CollectiveDayId,
                        principalTable: "CollectiveDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectiveProductSnapshots_ProductForCollectives_ProductForCollectiveId",
                        column: x => x.ProductForCollectiveId,
                        principalTable: "ProductForCollectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectiveOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectiveCustomerId = table.Column<int>(nullable: false),
                    CollectiveProductSnapshotId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 10, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Gift = table.Column<int>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyOrderId = table.Column<string>(maxLength: 20, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ordered = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiveOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectiveOrders_CollectiveCustomers_CollectiveCustomerId",
                        column: x => x.CollectiveCustomerId,
                        principalTable: "CollectiveCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectiveOrders_CollectiveProductSnapshots_CollectiveProductSnapshotId",
                        column: x => x.CollectiveProductSnapshotId,
                        principalTable: "CollectiveProductSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveCustomers_Name",
                table: "CollectiveCustomers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveCustomers_No",
                table: "CollectiveCustomers",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveCustomers_SubAreaId",
                table: "CollectiveCustomers",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveDays_Date",
                table: "CollectiveDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveOrders_CollectiveCustomerId",
                table: "CollectiveOrders",
                column: "CollectiveCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveOrders_CollectiveProductSnapshotId",
                table: "CollectiveOrders",
                column: "CollectiveProductSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveOrders_Date_CollectiveProductSnapshotId_CollectiveCustomerId",
                table: "CollectiveOrders",
                columns: new[] { "Date", "CollectiveProductSnapshotId", "CollectiveCustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveProductSnapshots_CollectiveDayId",
                table: "CollectiveProductSnapshots",
                column: "CollectiveDayId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiveProductSnapshots_ProductForCollectiveId",
                table: "CollectiveProductSnapshots",
                column: "ProductForCollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForCollectives_ProductId",
                table: "ProductForCollectives",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveOrders");

            migrationBuilder.DropTable(
                name: "CollectiveCustomers");

            migrationBuilder.DropTable(
                name: "CollectiveProductSnapshots");

            migrationBuilder.DropTable(
                name: "CollectiveDays");

            migrationBuilder.DropTable(
                name: "ProductForCollectives");
        }
    }
}
