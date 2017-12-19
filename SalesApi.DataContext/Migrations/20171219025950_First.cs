using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "DistributionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    No = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentTon = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(maxLength: 5, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ProductUnit = table.Column<int>(nullable: false),
                    ShelfLife = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetailDays",
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
                    table.PrimaryKey("PK_RetailDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 8, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Owner = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
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
                name: "ProductForRetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxPrice = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EquivalentBox = table.Column<int>(nullable: false),
                    InternalPrice = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    IsOrderByBox = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MinOrderCount = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrderDivisor = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 6)", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "DeliveryVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DistributionGroupId = table.Column<int>(nullable: false),
                    DistributionGroupId1 = table.Column<int>(nullable: true),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyAreaId = table.Column<string>(maxLength: 10, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    VehicleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryVehicles_DistributionGroups_DistributionGroupId",
                        column: x => x.DistributionGroupId,
                        principalTable: "DistributionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryVehicles_DistributionGroups_DistributionGroupId1",
                        column: x => x.DistributionGroupId1,
                        principalTable: "DistributionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryVehicles_Vehicles_VehicleId1",
                        column: x => x.VehicleId1,
                        principalTable: "Vehicles",
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
                name: "RetailProductSnapshots",
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
                    ProductForRetailId = table.Column<int>(nullable: false),
                    ProductUnit = table.Column<int>(nullable: false),
                    RetailDayId = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    ShelfLife = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(maxLength: 50, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(7, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailProductSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailProductSnapshots_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailProductSnapshots_RetailDays_RetailDayId",
                        column: x => x.RetailDayId,
                        principalTable: "RetailDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailPromotionSeries",
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
                    ProductForRetailId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    SalesType = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPromotionSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPromotionSeries_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeliveryVehicleId = table.Column<int>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacySubAreaId = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAreas_DeliveryVehicles_DeliveryVehicleId",
                        column: x => x.DeliveryVehicleId,
                        principalTable: "DeliveryVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailPromotionEvents",
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
                    ProductForRetailId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    RetailPromotionSeriesId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPromotionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPromotionEvents_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailPromotionEvents_RetailPromotionSeries_RetailPromotionSeriesId",
                        column: x => x.RetailPromotionSeriesId,
                        principalTable: "RetailPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailPromotionSeriesBonuses",
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
                    ProductForRetailId = table.Column<int>(nullable: false),
                    RetailPromotionSeriesId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPromotionSeriesBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPromotionSeriesBonuses_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailPromotionSeriesBonuses_RetailPromotionSeries_RetailPromotionSeriesId",
                        column: x => x.RetailPromotionSeriesId,
                        principalTable: "RetailPromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Retailers",
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
                    table.PrimaryKey("PK_Retailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retailers_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailPromotionEventBonuses",
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
                    ProductForRetailId = table.Column<int>(nullable: false),
                    RetailPromotionEventId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPromotionEventBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPromotionEventBonuses_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailPromotionEventBonuses_RetailPromotionEvents_RetailPromotionEventId",
                        column: x => x.RetailPromotionEventId,
                        principalTable: "RetailPromotionEvents",
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
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "CollectivePrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectiveCustomerId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductForCollectiveId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectivePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectivePrices_CollectiveCustomers_CollectiveCustomerId",
                        column: x => x.CollectiveCustomerId,
                        principalTable: "CollectiveCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectivePrices_ProductForCollectives_ProductForCollectiveId",
                        column: x => x.ProductForCollectiveId,
                        principalTable: "ProductForCollectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    RetailProductSnapshotId = table.Column<int>(nullable: false),
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
                        name: "FK_RetailOrders_RetailProductSnapshots_RetailProductSnapshotId",
                        column: x => x.RetailProductSnapshotId,
                        principalTable: "RetailProductSnapshots",
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
                name: "IX_CollectivePrices_ProductForCollectiveId",
                table: "CollectivePrices",
                column: "ProductForCollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePrices_CollectiveCustomerId_ProductForCollectiveId",
                table: "CollectivePrices",
                columns: new[] { "CollectiveCustomerId", "ProductForCollectiveId" },
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
                name: "IX_DeliveryVehicles_DistributionGroupId",
                table: "DeliveryVehicles",
                column: "DistributionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_DistributionGroupId1",
                table: "DeliveryVehicles",
                column: "DistributionGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_VehicleId",
                table: "DeliveryVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_VehicleId1",
                table: "DeliveryVehicles",
                column: "VehicleId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId",
                table: "DeliveryVehicles",
                columns: new[] { "SalesType", "VehicleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionGroups_No",
                table: "DistributionGroups",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForCollectives_ProductId",
                table: "ProductForCollectives",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForRetails_ProductId",
                table: "ProductForRetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailDays_Date",
                table: "RetailDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retailers_Name",
                table: "Retailers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retailers_No",
                table: "Retailers",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retailers_SubAreaId",
                table: "Retailers",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailOrders_RetailProductSnapshotId",
                table: "RetailOrders",
                column: "RetailProductSnapshotId");

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
                name: "IX_RetailOrders_Date_RetailProductSnapshotId_RetailerId",
                table: "RetailOrders",
                columns: new[] { "Date", "RetailProductSnapshotId", "RetailerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailProductSnapshots_ProductForRetailId",
                table: "RetailProductSnapshots",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailProductSnapshots_RetailDayId",
                table: "RetailProductSnapshots",
                column: "RetailDayId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEventBonuses_ProductForRetailId",
                table: "RetailPromotionEventBonuses",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEventBonuses_RetailPromotionEventId",
                table: "RetailPromotionEventBonuses",
                column: "RetailPromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId",
                table: "RetailPromotionEvents",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEvents_RetailPromotionSeriesId",
                table: "RetailPromotionEvents",
                column: "RetailPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionSeries_ProductForRetailId",
                table: "RetailPromotionSeries",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionSeriesBonuses_ProductForRetailId",
                table: "RetailPromotionSeriesBonuses",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionSeriesBonuses_RetailPromotionSeriesId",
                table: "RetailPromotionSeriesBonuses",
                column: "RetailPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDays_Date",
                table: "SalesDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_DeliveryVehicleId_Name",
                table: "SubAreas",
                columns: new[] { "DeliveryVehicleId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Name",
                table: "Vehicles",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiveOrders");

            migrationBuilder.DropTable(
                name: "CollectivePrices");

            migrationBuilder.DropTable(
                name: "RetailOrders");

            migrationBuilder.DropTable(
                name: "RetailPromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "SalesDays");

            migrationBuilder.DropTable(
                name: "CollectiveProductSnapshots");

            migrationBuilder.DropTable(
                name: "CollectiveCustomers");

            migrationBuilder.DropTable(
                name: "RetailProductSnapshots");

            migrationBuilder.DropTable(
                name: "Retailers");

            migrationBuilder.DropTable(
                name: "RetailPromotionEvents");

            migrationBuilder.DropTable(
                name: "CollectiveDays");

            migrationBuilder.DropTable(
                name: "ProductForCollectives");

            migrationBuilder.DropTable(
                name: "RetailDays");

            migrationBuilder.DropTable(
                name: "SubAreas");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeries");

            migrationBuilder.DropTable(
                name: "DeliveryVehicles");

            migrationBuilder.DropTable(
                name: "ProductForRetails");

            migrationBuilder.DropTable(
                name: "DistributionGroups");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
