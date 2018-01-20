﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class Initial : Migration
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
                    FullPinyin = table.Column<string>(maxLength: 50, nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    LegacyProductId = table.Column<string>(maxLength: 5, nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Pinyin = table.Column<string>(maxLength: 10, nullable: false),
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
                name: "SubscriptionDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 10, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDays", x => x.Id);
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
                name: "SubscriptionMonthPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    DayBase = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EndDate = table.Column<int>(nullable: true),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    PromotionType = table.Column<int>(nullable: false),
                    StartDate = table.Column<int>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotions_ProductForSubscriptions_ProductForSubscriptionId",
                        column: x => x.ProductForSubscriptionId,
                        principalTable: "ProductForSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "SubscriptionMonthPromotionBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PresetDayBonusCount = table.Column<int>(nullable: false),
                    ProductForSubscriptionId = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotionBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotions_SubscriptionMonthPromotionId",
                        column: x => x.SubscriptionMonthPromotionId,
                        principalTable: "SubscriptionMonthPromotions",
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
                name: "SubscriptionMonthPromotionBonusDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DayBonusCount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionBonusId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMonthPromotionBonusDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionBonusId",
                        column: x => x.SubscriptionMonthPromotionBonusId,
                        principalTable: "SubscriptionMonthPromotionBonuses",
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
                name: "SubscriptionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    MilkmanId = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PresetDayBonus = table.Column<int>(nullable: false),
                    PresetDayCount = table.Column<int>(nullable: false),
                    PresetDayGift = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionId = table.Column<int>(nullable: true),
                    SubscriptionProductSnapshotId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_Milkmen_MilkmanId",
                        column: x => x.MilkmanId,
                        principalTable: "Milkmen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_SubscriptionMonthPromotions_SubscriptionMonthPromotionId",
                        column: x => x.SubscriptionMonthPromotionId,
                        principalTable: "SubscriptionMonthPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrders_SubscriptionProductSnapshots_SubscriptionProductSnapshotId",
                        column: x => x.SubscriptionProductSnapshotId,
                        principalTable: "SubscriptionProductSnapshots",
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "CountyPromotionGiftOrder",
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
                    table.PrimaryKey("PK_CountyPromotionGiftOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountyPromotionGiftOrder_CountyOrders_CountyOrderId",
                        column: x => x.CountyOrderId,
                        principalTable: "CountyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountyPromotionGiftOrder_CountyPromotionEventBonuses_CountyPromotionEventBonusId",
                        column: x => x.CountyPromotionEventBonusId,
                        principalTable: "CountyPromotionEventBonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionOrderBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionMonthPromotionBonusDateId = table.Column<int>(nullable: false),
                    SubscriptionOrderId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrderBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusDateId",
                        column: x => x.SubscriptionMonthPromotionBonusDateId,
                        principalTable: "SubscriptionMonthPromotionBonusDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderBonuses_SubscriptionOrders_SubscriptionOrderId",
                        column: x => x.SubscriptionOrderId,
                        principalTable: "SubscriptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionOrderDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionOrderId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrderDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderDates_SubscriptionOrders_SubscriptionOrderId",
                        column: x => x.SubscriptionOrderId,
                        principalTable: "SubscriptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_CountyPromotionEvents_ProductForCountyId_Date",
                table: "CountyPromotionEvents",
                columns: new[] { "ProductForCountyId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrder_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder",
                column: "CountyPromotionEventBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyPromotionGiftOrder_CountyOrderId_CountyPromotionEventBonusId",
                table: "CountyPromotionGiftOrder",
                columns: new[] { "CountyOrderId", "CountyPromotionEventBonusId" },
                unique: true);

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
                name: "IX_Milkmen_SubAreaId",
                table: "Milkmen",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForCollectives_ProductId",
                table: "ProductForCollectives",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForCounties_ProductId",
                table: "ProductForCounties",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForMalls_ProductId",
                table: "ProductForMalls",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForRetails_ProductId",
                table: "ProductForRetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductForSubscriptions_ProductId",
                table: "ProductForSubscriptions",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailDays_Date",
                table: "RetailDays",
                column: "Date",
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
                name: "IX_RetailPromotionEvents_RetailPromotionSeriesId",
                table: "RetailPromotionEvents",
                column: "RetailPromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionEvents_ProductForRetailId_Date",
                table: "RetailPromotionEvents",
                columns: new[] { "ProductForRetailId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionGiftOrder_RetailPromotionEventBonusId",
                table: "RetailPromotionGiftOrder",
                column: "RetailPromotionEventBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPromotionGiftOrder_RetailOrderId_RetailPromotionEventBonusId",
                table: "RetailPromotionGiftOrder",
                columns: new[] { "RetailOrderId", "RetailPromotionEventBonusId" },
                unique: true);

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
                name: "IX_SubscriptionDays_Date",
                table: "SubscriptionDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonusDates_SubscriptionMonthPromotionBonusId_Date",
                table: "SubscriptionMonthPromotionBonusDates",
                columns: new[] { "SubscriptionMonthPromotionBonusId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotionBonuses_SubscriptionMonthPromotionId_ProductForSubscriptionId_PresetDayBonusCount",
                table: "SubscriptionMonthPromotionBonuses",
                columns: new[] { "SubscriptionMonthPromotionId", "ProductForSubscriptionId", "PresetDayBonusCount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMonthPromotions_ProductForSubscriptionId",
                table: "SubscriptionMonthPromotions",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                column: "SubscriptionMonthPromotionBonusDateId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderBonuses_SubscriptionOrderId_SubscriptionMonthPromotionBonusDateId",
                table: "SubscriptionOrderBonuses",
                columns: new[] { "SubscriptionOrderId", "SubscriptionMonthPromotionBonusDateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderDates_SubscriptionOrderId_Date",
                table: "SubscriptionOrderDates",
                columns: new[] { "SubscriptionOrderId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_MilkmanId",
                table: "SubscriptionOrders",
                column: "MilkmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_SubscriptionMonthPromotionId",
                table: "SubscriptionOrders",
                column: "SubscriptionMonthPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrders_SubscriptionProductSnapshotId",
                table: "SubscriptionOrders",
                column: "SubscriptionProductSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionProductSnapshots_ProductForSubscriptionId",
                table: "SubscriptionProductSnapshots",
                column: "ProductForSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionProductSnapshots_SubscriptionDayId",
                table: "SubscriptionProductSnapshots",
                column: "SubscriptionDayId");

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
                name: "CountyAgentPrices");

            migrationBuilder.DropTable(
                name: "CountyPromotionGiftOrder");

            migrationBuilder.DropTable(
                name: "CountyPromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "MallOrders");

            migrationBuilder.DropTable(
                name: "MallPrices");

            migrationBuilder.DropTable(
                name: "RetailPromotionGiftOrder");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "SalesDays");

            migrationBuilder.DropTable(
                name: "SubscriptionOrderBonuses");

            migrationBuilder.DropTable(
                name: "SubscriptionOrderDates");

            migrationBuilder.DropTable(
                name: "CollectiveProductSnapshots");

            migrationBuilder.DropTable(
                name: "CollectiveCustomers");

            migrationBuilder.DropTable(
                name: "CountyOrders");

            migrationBuilder.DropTable(
                name: "CountyPromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "MallProductSnapshots");

            migrationBuilder.DropTable(
                name: "MallCustomers");

            migrationBuilder.DropTable(
                name: "RetailOrders");

            migrationBuilder.DropTable(
                name: "RetailPromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotionBonusDates");

            migrationBuilder.DropTable(
                name: "SubscriptionOrders");

            migrationBuilder.DropTable(
                name: "CollectiveDays");

            migrationBuilder.DropTable(
                name: "ProductForCollectives");

            migrationBuilder.DropTable(
                name: "CountyAgents");

            migrationBuilder.DropTable(
                name: "CountyProductSnapshots");

            migrationBuilder.DropTable(
                name: "CountyPromotionEvents");

            migrationBuilder.DropTable(
                name: "MallDays");

            migrationBuilder.DropTable(
                name: "ProductForMalls");

            migrationBuilder.DropTable(
                name: "MallGroups");

            migrationBuilder.DropTable(
                name: "RetailProductSnapshots");

            migrationBuilder.DropTable(
                name: "Retailers");

            migrationBuilder.DropTable(
                name: "RetailPromotionEvents");

            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotionBonuses");

            migrationBuilder.DropTable(
                name: "Milkmen");

            migrationBuilder.DropTable(
                name: "SubscriptionProductSnapshots");

            migrationBuilder.DropTable(
                name: "CountyDays");

            migrationBuilder.DropTable(
                name: "CountyPromotionSeries");

            migrationBuilder.DropTable(
                name: "RetailDays");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeries");

            migrationBuilder.DropTable(
                name: "SubscriptionMonthPromotions");

            migrationBuilder.DropTable(
                name: "SubAreas");

            migrationBuilder.DropTable(
                name: "SubscriptionDays");

            migrationBuilder.DropTable(
                name: "ProductForCounties");

            migrationBuilder.DropTable(
                name: "ProductForRetails");

            migrationBuilder.DropTable(
                name: "ProductForSubscriptions");

            migrationBuilder.DropTable(
                name: "DeliveryVehicles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "DistributionGroups");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
