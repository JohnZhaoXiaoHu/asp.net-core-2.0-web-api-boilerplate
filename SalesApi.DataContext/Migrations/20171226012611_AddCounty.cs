using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddCounty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId",
                table: "RetailOrders");

            migrationBuilder.CreateTable(
                name: "CountyAgents",
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
                    table.PrimaryKey("PK_CountyAgents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyAgents_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyDays",
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
                    table.PrimaryKey("PK_CountyDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductForCounties",
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
                    table.PrimaryKey("PK_ProductForCounties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForCounties_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyAgentPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountyAgentId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductForCountyId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyAgentPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyAgentPrices_CountyAgents_CountyAgentId",
                        column: x => x.CountyAgentId,
                        principalTable: "CountyAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyAgentPrices_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyProductSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    CountyDayId = table.Column<int>(nullable: false),
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
                    ProductForCountyId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_CountyProductSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyProductSnapshots_CountyDays_CountyDayId",
                        column: x => x.CountyDayId,
                        principalTable: "CountyDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyProductSnapshots_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyPromotionSeries",
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
                    ProductForCountyId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyPromotionSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionSeries_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyPromotionEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountyPromotionSeriesId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForCountyId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyPromotionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionEvents_CountyPromotionSeries_CountyPromotionSeriesId",
                        column: x => x.CountyPromotionSeriesId,
                        principalTable: "CountyPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyPromotionEvents_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyPromotionSeriesBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(nullable: false),
                    CountyPromotionSeriesId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForCountyId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyPromotionSeriesBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionSeriesBonuses_CountyPromotionSeries_CountyPromotionSeriesId",
                        column: x => x.CountyPromotionSeriesId,
                        principalTable: "CountyPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyPromotionSeriesBonuses_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountyAgentId = table.Column<int>(nullable: false),
                    CountyProductSnapshotId = table.Column<int>(nullable: false),
                    CountyPromotionEventId = table.Column<int>(nullable: true),
                    CountyPromotionEventId1 = table.Column<int>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 10, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Gift = table.Column<int>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyOrderId = table.Column<string>(maxLength: 20, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Ordered = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyOrders_CountyAgents_CountyAgentId",
                        column: x => x.CountyAgentId,
                        principalTable: "CountyAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyOrders_CountyProductSnapshots_CountyProductSnapshotId",
                        column: x => x.CountyProductSnapshotId,
                        principalTable: "CountyProductSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyOrders_CountyPromotionEvents_CountyPromotionEventId",
                        column: x => x.CountyPromotionEventId,
                        principalTable: "CountyPromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyOrders_CountyPromotionEvents_CountyPromotionEventId1",
                        column: x => x.CountyPromotionEventId1,
                        principalTable: "CountyPromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountyPromotionEventBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(nullable: false),
                    CountyPromotionEventId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductForCountyId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyPromotionEventBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionEventBonuses_CountyPromotionEvents_CountyPromotionEventId",
                        column: x => x.CountyPromotionEventId,
                        principalTable: "CountyPromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyPromotionEventBonuses_ProductForCounties_ProductForCountyId",
                        column: x => x.ProductForCountyId,
                        principalTable: "ProductForCounties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountyAgentPrices_ProductForCountyId",
                table: "CountyAgentPrices",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyAgentPrices_CountyAgentId_ProductForCountyId",
                table: "CountyAgentPrices",
                columns: new[] { "CountyAgentId", "ProductForCountyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyAgents_No",
                table: "CountyAgents",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyAgents_SubAreaId",
                table: "CountyAgents",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyDays_Date",
                table: "CountyDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyOrders_CountyAgentId",
                table: "CountyOrders",
                column: "CountyAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyOrders_CountyProductSnapshotId",
                table: "CountyOrders",
                column: "CountyProductSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyOrders_CountyPromotionEventId",
                table: "CountyOrders",
                column: "CountyPromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyOrders_CountyPromotionEventId1",
                table: "CountyOrders",
                column: "CountyPromotionEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_CountyOrders_Date_CountyProductSnapshotId_CountyAgentId",
                table: "CountyOrders",
                columns: new[] { "Date", "CountyProductSnapshotId", "CountyAgentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyProductSnapshots_CountyDayId",
                table: "CountyProductSnapshots",
                column: "CountyDayId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyProductSnapshots_ProductForCountyId",
                table: "CountyProductSnapshots",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEventBonuses_CountyPromotionEventId",
                table: "CountyPromotionEventBonuses",
                column: "CountyPromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEventBonuses_ProductForCountyId",
                table: "CountyPromotionEventBonuses",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEvents_CountyPromotionSeriesId",
                table: "CountyPromotionEvents",
                column: "CountyPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionEvents_ProductForCountyId",
                table: "CountyPromotionEvents",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionSeries_ProductForCountyId",
                table: "CountyPromotionSeries",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionSeriesBonuses_CountyPromotionSeriesId",
                table: "CountyPromotionSeriesBonuses",
                column: "CountyPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionSeriesBonuses_ProductForCountyId",
                table: "CountyPromotionSeriesBonuses",
                column: "ProductForCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForCounties_ProductId",
                table: "ProductForCounties",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId",
                table: "RetailOrders",
                column: "RetailPromotionEventId",
                principalTable: "RetailPromotionEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId",
                table: "RetailOrders");

            migrationBuilder.DropTable(
                name: "CountyAgentPrices");

            migrationBuilder.DropTable(
                name: "CountyOrders");

            migrationBuilder.DropTable(
                name: "CountyPromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "CountyPromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "CountyAgents");

            migrationBuilder.DropTable(
                name: "CountyProductSnapshots");

            migrationBuilder.DropTable(
                name: "CountyPromotionEvents");

            migrationBuilder.DropTable(
                name: "CountyDays");

            migrationBuilder.DropTable(
                name: "CountyPromotionSeries");

            migrationBuilder.DropTable(
                name: "ProductForCounties");

            migrationBuilder.AddForeignKey(
                name: "FK_RetailOrders_RetailPromotionEvents_RetailPromotionEventId",
                table: "RetailOrders",
                column: "RetailPromotionEventId",
                principalTable: "RetailPromotionEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
