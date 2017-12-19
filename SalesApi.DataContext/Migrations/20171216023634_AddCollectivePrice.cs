using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddCollectivePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePrices_ProductForCollectiveId",
                table: "CollectivePrices",
                column: "ProductForCollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectivePrices_CollectiveCustomerId_ProductForCollectiveId",
                table: "CollectivePrices",
                columns: new[] { "CollectiveCustomerId", "ProductForCollectiveId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectivePrices");
        }
    }
}
