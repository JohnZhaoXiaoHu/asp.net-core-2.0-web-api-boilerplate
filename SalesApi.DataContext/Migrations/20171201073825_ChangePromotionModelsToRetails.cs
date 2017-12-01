using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class ChangePromotionModelsToRetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "PromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "PromotionEvents");

            migrationBuilder.DropTable(
                name: "PromotionSeries");

            migrationBuilder.CreateTable(
                name: "RetailPromotionSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductForRetailId = table.Column<int>(type: "int", nullable: false),
                    PurchaseBase = table.Column<int>(type: "int", nullable: false),
                    SalesType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "RetailPromotionEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductForRetailId = table.Column<int>(type: "int", nullable: false),
                    PurchaseBase = table.Column<int>(type: "int", nullable: false),
                    RetailPromotionSeriesId = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductForRetailId = table.Column<int>(type: "int", nullable: false),
                    RetailPromotionSeriesId = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "RetailPromotionEventBonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusCount = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductForRetailId = table.Column<int>(type: "int", nullable: false),
                    RetailPromotionEventId = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetailPromotionEventBonuses");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeriesBonuses");

            migrationBuilder.DropTable(
                name: "RetailPromotionEvents");

            migrationBuilder.DropTable(
                name: "RetailPromotionSeries");

            migrationBuilder.CreateTable(
                name: "PromotionSeries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductForRetailId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionSeries_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionEvents",
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
                    PromotionSeriesId = table.Column<int>(nullable: false),
                    PurchaseBase = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionEvents_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionEvents_PromotionSeries_PromotionSeriesId",
                        column: x => x.PromotionSeriesId,
                        principalTable: "PromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionSeriesBonuses",
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
                    PromotionSeriesId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSeriesBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionSeriesBonuses_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionSeriesBonuses_PromotionSeries_PromotionSeriesId",
                        column: x => x.PromotionSeriesId,
                        principalTable: "PromotionSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionEventBonuses",
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
                    PromotionEventId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionEventBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionEventBonuses_ProductForRetails_ProductForRetailId",
                        column: x => x.ProductForRetailId,
                        principalTable: "ProductForRetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionEventBonuses_PromotionEvents_PromotionEventId",
                        column: x => x.PromotionEventId,
                        principalTable: "PromotionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEventBonuses_ProductForRetailId",
                table: "PromotionEventBonuses",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEventBonuses_PromotionEventId",
                table: "PromotionEventBonuses",
                column: "PromotionEventId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEvents_ProductForRetailId",
                table: "PromotionEvents",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEvents_PromotionSeriesId",
                table: "PromotionEvents",
                column: "PromotionSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSeries_ProductForRetailId",
                table: "PromotionSeries",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSeriesBonuses_ProductForRetailId",
                table: "PromotionSeriesBonuses",
                column: "ProductForRetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSeriesBonuses_PromotionSeriesId",
                table: "PromotionSeriesBonuses",
                column: "PromotionSeriesId");
        }
    }
}
